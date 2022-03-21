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
        // [Route("PreuzmiZanrove")]
        // [HttpGet]
        // public async Task<ActionResult> PreuzmiZanrove()
        // {
        //     try
        //     {
                
        //         var zanrovi = Context.Prodavnice.Include(p => p.ploce).Distinct().ToList();
                
        //            return Ok
        //          (
        //               zanrovi.Select(p=>
        //                   new
        //                   {
        //                       naziv=p.Zanr
                             

        //               }).ToList();
        //     })
    

        //     catch (Exception e)
        //     {

        //         return BadRequest(e.Message);
        //     }
        // }


        // [Route("PreuzmiLepo")]
        // [HttpGet]
        // public async Task<ActionResult> Preuzmi(/*[FromQuery] int[] IDs*/) //neke stvari o vezanim klasama, sve sto je potrebno 
        // {
        //     // Include(p => p.StudentPredmet.Where...) omogućava uključivanje podatka samo za određene studente
        //     var vinyl = Context.Ploce //radi sa student objektom
        //         .Include(p => p.prodavnica) //radi s onima sto je prethodno ukljuceno
        //         .Include(p => p.izvodjac)
        //         .Include(p => p.izdavackaKuca);

        //     var ploca = await vinyl.ToListAsync();
        //     return Ok
        //     (
        //         ploca.Select(q =>
        //         new
        //         {
        //             Ime = q.Ime,
        //             IzvodjacIme = q.izvodjac.Ime,
        //             IzvodjacPrezime = q.izvodjac.Prezime,
        //             Pesme = q.Pesme,
        //             Zanr = q.Zanr,
        //             GodinaStampanja = q.GodinaStampanja,
        //             NazivKuce = q.izdavackaKuca.Ime,
        //             Cena = q.prodavnica.Cena

        //         }).ToList()
        //  );


        //     // catch (Exception e)
        //     // {

        //     //     return BadRequest(e.Message);
        //     // }
        // }

        [Route("PrikaziPesme/{Ime}/{ImeIzvodjaca}")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiPesme(string ImePloce, string ImeIzvodjaca)
        {
             var vinyl = await Context.Ploce
                    .Include(p => p.izvodjac)
                    .Where(p => p.Ime == ImePloce &&
                    p.izvodjac.Ime == ImeIzvodjaca ).FirstOrDefaultAsync();

            if (vinyl != null)
                return Ok(vinyl.Pesme);
            else
                return BadRequest("Nije pronadjena ploca!");

            // catch (Exception e)
            // {

            //     return BadRequest(e.Message);
            // }
        }


        // //////////////////////////////////////////////POST////////////////////////////////////////////////

        // [Route("DodajPlocu")]
        // [HttpPost]
        // public async Task<ActionResult> DodajPlocu([FromBody] Vinyl vinyl) //cela ploca!!
        // {
        //     if (string.IsNullOrWhiteSpace(vinyl.Ime) || vinyl.Ime.Length > 50)//bar jedan karakter
        //     {
        //         return BadRequest("Pogrešno ime!");
        //     }

        //     if (vinyl.GodinaStampanja < 1930 || vinyl.GodinaStampanja > 2022)
        //     {
        //         return BadRequest("Uneti validnu godinu štampanja ploče!");
        //     }

        //     if (string.IsNullOrWhiteSpace(vinyl.Pesme))
        //     {
        //         return BadRequest("Niste uneli pesme!");
        //     }

        //     if (!Enum.IsDefined(typeof(Zanr), vinyl.Zanr))
        //     {
        //         return BadRequest("Uneti validan zanr ploče.");
        //     }



        //     try
        //     {
        //         Context.Ploce.Add(vinyl); //ne zove bazu sad
        //         await Context.SaveChangesAsync(); //nego ovde i apdejtuje model, prekopirace u bazu, validan id, u pozadini JavaScripta
        //         return Ok($"Ploča je dodata sa ID: {vinyl.ID}"); //mora da se upise ID, vraca Task(int)
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
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

        [Route("PromenitiVinyl/{ime}/{godinaStampanja}")] //posebne podatke da promenimo
        [HttpPut]
        public async Task<ActionResult> Promeni(string ime, int godinaStampanja)
        {
            if (godinaStampanja < 1931 || godinaStampanja > 2022)
            {
                return BadRequest("Pogrešna godina štampanja!");
            }
            if (ime.Length > 50)
            {
                return BadRequest("Ime ploče ne valja.");
            }
            try
            {
                var vinyl = Context.Ploce.Where(p => p.Ime == ime).FirstOrDefault();
                //vratiti prvog koji zadovoljava uslove ili null ako ne postoji
                //var zakljuci sam
                //var zamenjuje bilo koji tip

                if (vinyl != null)
                {
                    vinyl.GodinaStampanja = godinaStampanja;//trazi po godini stampanja
                    vinyl.Ime = ime;

                    await Context.SaveChangesAsync(); //salju se promene u bazu podataka
                    return Ok($"Uspešno promenjena ploča! ID: {vinyl.ID}");
                }
                else
                {
                    return BadRequest("Ploča nije pronađena!");
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromenaFromBody")]
        [HttpPut]
        public async Task<ActionResult> PromeniBody([FromBody] Vinyl vinyl)
        {
            if (vinyl.ID <= 0) //posalje se i ID
            {
                return BadRequest("Pogrešan ID!");
            }

            // ... Ostale provere (Indeks, Ime, Prezime)

            try
            {
                // var plocaZaPromenu = await Context.Ploce.FindAsync(vinyl.ID); //vrata student u slucaju da postoji ID koji smo prosledili, prihvata primarni kljuc od vise razlicitih vrednosti 
                // plocaZaPromenu.Ime = vinyl.Ime;
                // plocaZaPromenu.GodinaStampanja = vinyl.GodinaStampanja;

                Context.Ploce.Update(vinyl); //ovo je druga opcija i prosledi se sam student, mora da bude ispravan ID!

                await Context.SaveChangesAsync();
                return Ok($"Ploca sa ID: {vinyl.ID} je uspešno izmenjena!"); //{studentZaPromenu.ID}
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        ////////////////////////////////////////////////DELETE/////////////////////////////////////////////////

        [Route("ObrisiPlocu/{id}")]
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



