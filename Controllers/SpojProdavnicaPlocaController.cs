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
    public class SpojProdavnicaPlocaController : ControllerBase
    {
        
        public VinylContext Context { get; set; }

        public SpojProdavnicaPlocaController(VinylContext context)
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

        
        // //////////////////////////////////////////////POST////////////////////////////////////////////////

        [Route("DodajPlocu/{nazivProdavnice}/{nazivPloce}/{izvodjac}/{godinastampanja}/{zanr}/{pesme}/{cena}/{kolicina}")] //dobaaaaaaaaaaaar
        [HttpPost]
        public async Task<ActionResult> DodajPlocu([FromRoute]string nazivProdavnice, string nazivPloce, string izvodjac, int godinastampanja, Zanr zanr, string pesme, int cena, int kolicina) //cela ploca!!
        {
            if (string.IsNullOrWhiteSpace(nazivProdavnice) || nazivProdavnice.Length > 50)//bar jedan karakter
            {
                return BadRequest("Pogrešno ime prodavnica!");
            }

            if (string.IsNullOrWhiteSpace(nazivPloce) || nazivPloce.Length > 50)//bar jedan karakter
            {
                return BadRequest("Loše ime prodavnica!");
            }

            if (godinastampanja < 1930 || godinastampanja > 2022)
            {
                return BadRequest("Uneti validnu godinu štampanja ploče!");
            }

            if (pesme==null)
            {
                return BadRequest("Niste uneli pesme!");
            }
            if(cena<0)
            {
                return BadRequest("Cena nije validna!");
            }
            if(kolicina<1)
            {
                return BadRequest("Kolicina nije validna!");
            }


            var prodavnica = await Context.Prodavnice
            .Where(p=> p.Naziv == nazivProdavnice)
            // .Select(p=>p.ID)
            .FirstOrDefaultAsync();

            if(prodavnica==null)
            return BadRequest("Nije nadjena prodavnica");

            var i = await Context.Izvodjaci
            .Where(p=> p.Ime == izvodjac)
            // .Select(p=>p.ID)
            .FirstOrDefaultAsync();

            
            if(i==null){
                var novi=new Izvodjac{
                    Ime=izvodjac
                };
                i=novi;
                Context.Izvodjaci.Add(i);
                await Context.SaveChangesAsync();
            }


            try
            {
                var Vinyl=new Vinyl{
                Ime=nazivPloce,
                Zanr=zanr,
                GodinaStampanja=godinastampanja,
                izvodjac=i,
                Pesme=pesme
                };
                Context.Ploce.Add(Vinyl);
                await Context.SaveChangesAsync();


                var spoj = new SpojProdavnicaPloca{
                    Cena=cena,
                    Kolicina=kolicina,
                    ploca=Vinyl,
                    prodavnica=prodavnica
                };
                Context.ProdavnicaPloca.Add(spoj);
                await Context.SaveChangesAsync();

                i.ploce.Add(Vinyl);
             return Ok("Ploča je dodata!!!!!!!!!!!!!"); 
                
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        // [Route("Plocu_dodeli_+Prodavnicu_+Izvodjaca/{ime}/{zanr}/{godinastampanja}/{pesme}/{prodavnica}/{izvodjac}")]
        // [HttpPost]
        // public async Task<ActionResult> DodajPsaUzmiIDDodeliAzil(string ime, Zanr zanr, int godinastampanja, string pesme, string prodavnica, string izvodjac, string izvodjacp)
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

            
           
        //     var p= await Context.Prodavnice.Where(q=>q.prodavnica.Naziv==prodavnica.Naziv).FirstOrDefaultAsync();
        //     var i=await Context.Izvodjaci.Where(q=>(q.Ime==izvodjac && q.Prezime==izvodjacp)).FirstOrDefaultAsync();
            

        //     // var izdavacka_kuca=await Context.IzdavackeKuce.Where(q=>q.Ime==ik).FirstOrDefaultAsync();
        //     if(i==null)
        //     {
        //         i= new Izvodjac{
        //         Ime=izvodjac,
        //         Prezime=izvodjacp
        //         };

        //         Context.Izvodjaci.Add(i);
        //           Context.SaveChanges();

        //     }
        //     // if(izdavacka_kuca==null)
        //     // {
        //     //     izdavacka_kuca=new IzdavackaKuca{
        //     //     Ime=ik
        //     // };

        //     //     Context.IzdavackeKuce.Add(izdavacka_kuca);
        //     //       Context.SaveChanges();

        //     // }
        //     try
        //     {

        //         var Vinyl=new Vinyl{
        //         Ime=ime,
        //         Zanr=zanr,
        //         GodinaStampanja=godinastampanja,
        //         Pesme=pesme,
        //         izvodjac=i,
        //         prodavnica.prodavnica=p,
        //         // izdavackaKuca=izdavacka_kuca
        //         };
            

        //         Context.Ploce.Add(Vinyl);
        //         await Context.SaveChangesAsync();//da bi se obavljalo u pozadinskoj niti,neblokirajuca metoda
        //         //vraca broj podataka koje smo upisali
        //          return Ok(Vinyl);
                
        //     }
        //     catch (Exception e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
        ////////////////////////////////////////////////////PUT///////////////////////////////////////////////////

        
    }
}


