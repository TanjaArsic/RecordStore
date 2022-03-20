using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace wyyybbb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdavacController : ControllerBase
    {
        public VinylContext Context { get; set; }

        public ProdavacController(VinylContext context)
        {
            Context = context;
        }

        [Route("Prodavac")]
        [HttpGet]
        public async Task<ActionResult> Prodavci()
        {
            try
            {
                return Ok(await Context.Prodavci.Select(p =>
                new
                {
                    ID = p.ID,
                    Ime = p.Ime,
                    Prezime = p.Prezime,
                    BrojTelefona = p.BrojTelefona,
                    LicnaKarta=p.LicnaKarta
                }).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [EnableCors("CORS")]
        [Route("PrikaziProdavca/{imeProdavnice}")]
        [HttpGet]
        public async Task<ActionResult> PrikaziProdavca(string imeProdavnice)
        {
            try
            {
                var prod = await Context.Prodavnice
                .Include(p=>p.prodavci)
                .Where(p=>p.Naziv==imeProdavnice)
                .FirstOrDefaultAsync();
                return Ok(prod.prodavci.Select(q =>
                new
                {
                    ID=q.ID,
                    Ime=q.Ime,
                    Prezime=q.Prezime,
                    BrojTelefona=q.BrojTelefona,
                    LicnaKarta=q.LicnaKarta

                }));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("DodajProdavca/{ime}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DodajProdavca(string ime)
        {
            if (string.IsNullOrWhiteSpace(ime))
            {
                return BadRequest("Pogrešno ime prodavca!");
            }

            try
            {
                Prodavac prodavac = new Prodavac
                {
                    Ime = ime
                };

                Context.Prodavci.Add(prodavac);
                await Context.SaveChangesAsync();
                return Ok("Uspešno zaposlen prodavac!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PromenitiBrojTelefonaProdavca/{ime}/{brojTelefona}")] //posebne podatke da promenimo
        [HttpPut]
        public async Task<ActionResult> Promeni(string ime, string brojTelefona)
        {
            
            if(ime.Length > 50)
            {
                return BadRequest("Ime prodavca ne valja.");
            }
            try
            {
                var prodavac = Context.Prodavci.Where(p => p.Ime == ime).FirstOrDefault(); 
                //vratiti prvog koji zadovoljava uslove ili null ako ne postoji
                //var zakljuci sam
                //var zamenjuje bilo koji tip

                if (prodavac != null)
                {
                    prodavac.BrojTelefona = brojTelefona;

                    await Context.SaveChangesAsync(); //salju se promene u bazu podataka
                    return Ok($"Uspešno promenjen broj telefona prodavca! ID: {prodavac.ID}");
                }
                else
                {
                    return BadRequest("Prodavac nije pronađena!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message); 
            }
        }
    }
}