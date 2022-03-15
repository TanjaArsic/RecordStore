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
    public class ProdavnicaController : ControllerBase
    {
        public VinylContext Context { get; set; }

        public ProdavnicaController(VinylContext context)
        {
            Context = context;
        }

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
                    BrojVinyla = p.BrVinyl,
                    Mail=p.Mail
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

        [Route("DodajProdavnicuSVE")]
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

        [Route("DodajProdavnicu/{ime}")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> DodajProdavnicu(string ime)
        {
            if (string.IsNullOrWhiteSpace(ime))
            {
                return BadRequest("Pogrešno ime prodavnice!");
            }

            try
            {
                Prodavnica prodavnica = new Prodavnica
                {
                    Naziv = ime
                };

                Context.Prodavnice.Add(prodavnica);
                await Context.SaveChangesAsync();
                return Ok("Uspešno kreirana prodavnica!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [Route("PromenitiBrojPlocaUProdavnici/{ime}/{BrojVinyla}")] //posebne podatke da promenimo
        [HttpPut]
        public async Task<ActionResult> Promeni(string ime, int BrojVinyla)
        {
            
            if(ime.Length > 50)
            {
                return BadRequest("Ime prodavnice ne valja.");
            }
            try
            {
                var prodavnica = Context.Prodavnice.Where(p => p.Naziv == ime).FirstOrDefault(); 
                //vratiti prvog koji zadovoljava uslove ili null ako ne postoji
                //var zakljuci sam
                //var zamenjuje bilo koji tip

                if (prodavnica != null)
                {
                    prodavnica.BrVinyl = BrojVinyla;

                    await Context.SaveChangesAsync(); //salju se promene u bazu podataka
                    return Ok($"Uspešno promenjen broj ploca u prodavnici. ID: {prodavnica.ID}");
                }
                else
                {
                    return BadRequest("Prodavnica nije pronađena!");
                }
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