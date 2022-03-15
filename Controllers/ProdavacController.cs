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

        // [Route("DodajPolozeniIspit/{indeks}/{idPredmeta}/{idRoka}/{ocena}")]
        // [HttpPost]
        // public async Task<ActionResult> DodajIspit(int indeks, int idPredmeta, int idRoka, int ocena)
        // {
        //     if (indeks < 10000 || indeks > 20000)
        //     {
        //         return BadRequest("Pogrešan broj indeksa");
        //     }
        //     //...

        //     try
        //     {
        //         var student = await Context.Studenti.Where(p => p.Indeks == indeks).FirstOrDefaultAsync();
        //         var predmet = await Context.Predmeti.Where(p => p.ID == idPredmeta).FirstOrDefaultAsync();
        //         var ispitniRok = await Context.Rokovi.FindAsync(idRoka);

        //         Spoj s = new Spoj
        //         {
        //             Student = student,
        //             Predmet = predmet,
        //             IspitniRok = ispitniRok,
        //             Ocena = ocena
        //         };

        //         Context.StudentiPredmeti.Add(s);
        //         await Context.SaveChangesAsync();

        //         var podaciOStudnetu = await Context.StudentiPredmeti
        //                 .Include(p => p.Student)
        //                 .Include(p => p.Predmet)
        //                 .Include(p => p.IspitniRok)
        //                 .Where(p => p.Student.Indeks == indeks)
        //                 .Select(p =>
        //                 new
        //                 {
        //                     Indeks = p.Student.Indeks,
        //                     Ime = p.Student.Ime,
        //                     Prezime = p.Student.Prezime,
        //                     Predmet = p.Predmet.Naziv,
        //                     IspitniRok = p.IspitniRok.Naziv,
        //                     Ocena = p.Ocena
        //                 }).ToListAsync();
        //         return Ok(podaciOStudnetu);
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

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