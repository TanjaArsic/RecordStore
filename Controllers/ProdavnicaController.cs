using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace wyyybbb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdavnicaController : ControllerBase
    {
        public VinylContext Context { get; set; }

        public ProdavnicaController(VinylContext context)
        {
            Context = context;
        }

        [EnableCors("CORS")]
        [Route("Prodavnica")]
        [HttpGet]
        public async Task<ActionResult> Prodavnice()
        {
            try
            {
                return Ok(await Context.Prodavnice.Select(p =>
                new
                {
                    ID = p.ID,
                    Naziv = p.Naziv,
                    Adresa = p.Adresa,
                    Mail=p.Mail
                }).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    

        [EnableCors("CORS")]
        [Route("Prodavnica+Ploce/{idProdavnice}")]
        [HttpGet]
        public async Task<ActionResult> ProdavnicaiPloce(string idProdavnice) //dobaaaaaaaaaaaar
        {
            if(idProdavnice==null){
                return BadRequest("Prodavnica ne postoji");
            }
            try
            {
                var prod = await Context.Prodavnice
                .Include(p=>p.ploce)
                .ThenInclude(p=>p.ploca)
                .ThenInclude(p=>p.izvodjac)
                .Where(p=>p.Naziv==idProdavnice)
                .FirstOrDefaultAsync();
                return Ok(prod.ploce.Select(q =>
                new
                {
                    Cena=q.Cena,
                    Kolicina=q.Kolicina,
                    Ploca=q.ploca,
                    Izvodjac=q.ploca.izvodjac,
                    Zanr=q.ploca.Zanr
                    

                }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }





        [Route("DodajProdavnicuSVE")] //dobaaaaaaaaaaaar
        [HttpPost]
        public async Task<ActionResult> DodajShop([FromBody] Prodavnica prodavnica) //cela ploca!!
        {
            if (string.IsNullOrWhiteSpace(prodavnica.Naziv) || prodavnica.Naziv.Length > 50)//bar jedan karakter
            {
                return BadRequest("Pogrešno ime prodavnice!");
            }
          
            if (string.IsNullOrWhiteSpace(prodavnica.Adresa) || prodavnica.Adresa.Length > 100)//bar jedan karakter
            {
                return BadRequest("Pogrešna adresa prodavnice!");
            }

            if (string.IsNullOrWhiteSpace(prodavnica.Mail) || prodavnica.Mail.Length > 100)//bar jedan karakter
            {
                return BadRequest("Pogrešna mail adresa prodavnice!");
            }


            try
            {
                Context.Prodavnice.Add(prodavnica); //ne zove bazu sad
                await Context.SaveChangesAsync(); //nego ovde i apdejtuje model, prekopirace u bazu, validan id, u pozadini JavaScripta
                return Ok($"Uspesno uneta prodavnica: {prodavnica.Naziv}"); //mora da se upise ID, vraca Task(int)
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


       


        [Route("IzbrisiProdavnicu")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisatiProdavnicu(int id)
        {
            if (id < 0)
            {
                return BadRequest("Nevalidan id!");
            }

            try
            {
                var prodavnica = await Context.Prodavnice.FindAsync(id);
                Context.Prodavnice.Remove(prodavnica);
                await Context.SaveChangesAsync();
                return Ok($"Uspešno izbrisana prodavnica {prodavnica.Naziv}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}