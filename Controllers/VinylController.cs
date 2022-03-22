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
    public class VinylController : ControllerBase
    {
        
        public VinylContext Context { get; set; }

        public VinylController(VinylContext context)
        {
            Context = context;
        }

        // // Cors policy može da se uključi i za pojedinačne metode, ovako
        // [EnableCors("CORS")]
        // // Ruta može da se razlikuje od naziva metode
        // [Route("Vinyls")]
        // [HttpGet]
        // [ProducesResponseType(StatusCodes.Status200OK)]
        // [ProducesResponseType(StatusCodes.Status400BadRequest)]

        /////////////////////////////////////////////GET////////////////////////////////////////////////////////////////
        [Route("Preuzmi")]
        [HttpGet]
        public ActionResult PreuzmiPlochu()
        {
            return Ok(Context.Ploce);
        }

        // [Route("PreuzmiZanr")]
        // [HttpGet]
        // public ActionResult PreuzmiZanr()
        // {
        //     return Ok(Context.Ploce.Zanr);
        // }


       


       
        // [EnableCors("CORS")]
        // [Route("DodatiVinyl")]
        // [HttpPost]
        // public async Task<ActionResult> DodajVinyl(string ime, Zanr zanr, int godinastampanja, string pesme) //prosledi se ceo student
        // {
        //     if (string.IsNullOrWhiteSpace(ime) || ime.Length > 50)//bar jedan karakter
        //     {
        //         return BadRequest("Pogrešno ime!");
        //     }

        //     if (godinastampanja < 1930 || godinastampanja > 2022)
        //     {
        //         return BadRequest("Uneti validnu godinu štampanja ploče!");
        //     }


        //     if (string.IsNullOrWhiteSpace(pesme))
        //     {
        //         return BadRequest("Niste uneli pesme!");
        //     }

        //     if (!Enum.IsDefined(typeof(Zanr), zanr))
        //     {
        //         return BadRequest("Uneti validan zanr ploče.");
        //     }

        //     try
        //     {
        //         var Vinyl=new Vinyl{
        //         Ime=ime,
        //         Zanr=zanr,
        //         GodinaStampanja=godinastampanja,
        //         Pesme=pesme
        //         };
        //         Context.Ploce.Add(Vinyl);
        //         await Context.SaveChangesAsync();

        //         return Ok($"Ploča je dodata sa ID: {Vinyl.ID}"); 
                
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        ////////////////////////////////////////////////////PUT///////////////////////////////////////////////////

        // [Route("PromenitiVinyl/{ime}/{godinaStampanja}/{pesme}")] //posebne podatke da promenimo
        // [HttpPut]
        // public async Task<ActionResult> Promeni(string ime, int godinaStampanja, string pesme)
        // {
        //     if (godinaStampanja < 1931 || godinaStampanja > 2022)
        //     {
        //         return BadRequest("Pogrešna godina štampanja!");
        //     }
        //     if (ime.Length > 50)
        //     {
        //         return BadRequest("Ime ploče ne valja.");
        //     }
        //     try
        //     {
        //         var vinyl = Context.Ploce.Where(p => p.Ime == ime).FirstOrDefault();
        //         //vratiti prvog koji zadovoljava uslove ili null ako ne postoji
        //         //var zakljuci sam
        //         //var zamenjuje bilo koji tip

        //         if (vinyl != null)
        //         {
        //             vinyl.GodinaStampanja = godinaStampanja;//trazi po godini stampanja
        //             vinyl.Ime = ime;
        //             vinyl.Pesme=pesme;

        //             await Context.SaveChangesAsync(); //salju se promene u bazu podataka
        //             return Ok($"Uspešno promenjena ploča! Ime: {vinyl.Ime}");
        //         }
        //         else
        //         {
        //             return BadRequest("Ploča nije pronađena!");
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        // [Route("PromenaFromBody")]
        // [HttpPut]
        // public async Task<ActionResult> PromeniBody([FromBody] Vinyl vinyl)
        // {
        //     if (vinyl.ID <= 0) //posalje se i ID
        //     {
        //         return BadRequest("Pogrešan ID!");

        //     }

        //     if(vinyl.Ime==null)
        //     {
        //         return BadRequest("Mora imati ime!");
        //     }

        //     if(vinyl.Pesme==null)
        //     {
        //         return BadRequest("Mora imati pesme!");
        //     }

        //     if(vinyl.GodinaStampanja<1930 || vinyl.GodinaStampanja>2022)
        //     {
        //         return BadRequest("Mora biti validna godina stampanja!");
        //     }

        //     // if(vinyl.Cena<0)
        //     // {
        //     //     return BadRequest("Ploca mora imati cenu");
        //     // }
   
        //     try
        //     {
        //         // var plocaZaPromenu = await Context.Ploce.FindAsync(vinyl.ID); //vrata student u slucaju da postoji ID koji smo prosledili, prihvata primarni kljuc od vise razlicitih vrednosti 
        //         // plocaZaPromenu.Ime = vinyl.Ime;
        //         // plocaZaPromenu.GodinaStampanja = vinyl.GodinaStampanja;

        //         Context.Ploce.Update(vinyl); //ovo je druga opcija i prosledi se sam student, mora da bude ispravan ID!

        //         await Context.SaveChangesAsync();
        //         return Ok($"Ploca sa imenom: {vinyl.Ime} je uspešno izmenjena!"); //{studentZaPromenu.ID}
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }


        [Route("IzmeniCenuPloce/{id}/{cena}")] //dobaaaaaaaaaaaar
        [HttpPut]
        public async Task<ActionResult> IzmeniPlocu(int id, int cena)
        {
            try
            {
                var vinyl = await Context.Ploce
                .Where(p => p.ID == id)
                .FirstOrDefaultAsync();

                if (vinyl == null) 
                
                    return BadRequest("Ploca ne postoji.");

                var pp = await Context.ProdavnicaPloca
                .Where(p => p.ploca == vinyl)
                .ToListAsync();

                foreach (SpojProdavnicaPloca ploca in pp)
                {
                    ploca.Cena=cena;
                    Context.ProdavnicaPloca.Update(ploca);
                }
                

                Context.Ploce.Update(vinyl);
                await Context.SaveChangesAsync();
                return Ok("Ploca je izmenjena!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

     
        ////////////////////////////////////////////////DELETE/////////////////////////////////////////////////

        [Route("ObrisiPlocu/{id}")] //dobaaaaaaaaaaaar
        [HttpDelete]
        public async Task<ActionResult> ObrisiPlocu(int id)
        {
            try
            {
                var vinyl = await Context.Ploce
                .Where(p => p.ID == id)
                .FirstOrDefaultAsync();

                if (vinyl == null) 
                
                    return BadRequest("Ploca ne postoji.");

                var pp = await Context.ProdavnicaPloca
                .Where(p => p.ploca == vinyl)
                .ToListAsync();

                foreach (SpojProdavnicaPloca ploca in pp)
                {
                    Context.ProdavnicaPloca.Remove(ploca);
                }

                Context.Ploce.Remove(vinyl);
                await Context.SaveChangesAsync();
                return Ok("Ploca je obrisana!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }

}



