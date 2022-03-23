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

    

        
    }
}


