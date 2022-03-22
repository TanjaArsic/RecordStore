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

        // [Route("Prodavac")]
        // [HttpGet]
        // public async Task<ActionResult> Prodavci()
        // {
        //     try
        //     {
        //         return Ok(await Context.Prodavci.Select(p =>
        //         new
        //         {
        //             ID = p.ID,
        //             Ime = p.Ime,
        //             Prezime = p.Prezime,
        //             BrojTelefona = p.BrojTelefona,
        //             LicnaKarta=p.LicnaKarta
        //         }).ToListAsync());
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }


        [EnableCors("CORS")]
        [Route("PrikaziProdavca/{imeProdavnice}")] //dobaaaaaaaaaaaar
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


        [Route("DodajProdavca/{imeProdavnice}/{ime}/{prezime}/{LicnaKarta}/{brojTelefona}")] ////dooobaaaarrrrr
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DodajProdavca(string imeProdavnice, string ime, string prezime, int LicnaKarta, string brojTelefona)
        {
            if (string.IsNullOrWhiteSpace(imeProdavnice))
            {
                return BadRequest("Uneti prodavnicu u kojoj radi prodavac!");
            }
            if (string.IsNullOrWhiteSpace(ime))
            {
                return BadRequest("Pogrešno ime prodavca!");
            }
            if (string.IsNullOrWhiteSpace(prezime))
            {
                return BadRequest("Pogrešno prezime prodavca!");
            }
            if (LicnaKarta.ToString().Length != 9)
            {
                return BadRequest("Uneti validnu ličnu kartu!");
            }
            if (brojTelefona.Length< 9 || brojTelefona.Length>10)
            {
                return BadRequest("Uneti validan broj telefona prodavca");
            }

            var prodavnica = await Context.Prodavnice
            .Where(p=> p.Naziv == imeProdavnice)
            .Include(p=>p.prodavci)
            .FirstOrDefaultAsync();

            if(prodavnica==null)
            return BadRequest("Nije nadjena prodavnica");


            Prodavac pr=new Prodavac();
            pr.prodavnica=prodavnica;
            pr.Ime=ime;
            pr.Prezime=prezime;
            pr.LicnaKarta=LicnaKarta;
            pr.BrojTelefona=brojTelefona;

            try
            {
                Context.Add(pr);
                await Context.SaveChangesAsync();
                return Ok(pr);
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }

        }

        [Route("IzbrisiProdavca")] //dobaaaaaaaaaaaar
        [HttpDelete]
        public async Task<ActionResult> IzbrisiProdavca(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }

            try
            {
                var prodavac = await Context.Prodavci.FindAsync(id);
                Context.Prodavci.Remove(prodavac);
                await Context.SaveChangesAsync();
                return Ok($"Uspešno otpušten prodavac {prodavac.Ime} {prodavac.Prezime}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    
        
    }
}