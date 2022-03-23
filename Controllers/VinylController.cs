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
    

        [Route("IzmeniCenuPloce/{naziv}/{izvodjac}/{cena}")] //
        [HttpPut]
        public async Task<ActionResult> IzmeniPlocu(string naziv, string izvodjac, int cena)//
        {
        
            var vinyl = await Context.Ploce
            .Where(p => p.Ime == naziv)
            .FirstOrDefaultAsync();

            var iz = await Context.Izvodjaci
            .Where(p => p.Ime == izvodjac)
            .FirstOrDefaultAsync();

            if (vinyl == null) 
            
                return BadRequest("Ploca ne postoji.");

            if(izvodjac==null)

                return BadRequest("Izvodjac ne postoji.");
         try
            {
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
        [Route("SmanjiKolicinuPloce/{naziv}/{izvodjac}")] //
        [HttpPut]
        public async Task<ActionResult> SmanjiKolicinuPlocu(string naziv, string izvodjac)
        {
           
            var vinyl = await Context.Ploce
            .Where(p => p.Ime == naziv)
            .FirstOrDefaultAsync();

            var iz = await Context.Izvodjaci
            .Where(p => p.Ime == izvodjac)
            .FirstOrDefaultAsync();

            if (vinyl == null) 
            
                return BadRequest("Ploca ne postoji.");

            if(izvodjac==null)

                return BadRequest("Izvodjac ne postoji.");

            try
            {
                var pp = await Context.ProdavnicaPloca
                .Where(p => p.ploca == vinyl)
                .ToListAsync();

                foreach (SpojProdavnicaPloca ploca in pp)
                {
                    ploca.Kolicina=ploca.Kolicina-1;
                    Context.ProdavnicaPloca.Update(ploca);
                }
                

                Context.Ploce.Update(vinyl);
                await Context.SaveChangesAsync();
                return Ok("Kolicina ploca je izmenjena!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        [Route("VratiZanrove")]
        [HttpGet]
        public async Task<ActionResult> PreuzmiZanrove([FromQuery] int[] podatak)
        {
            return Ok(podatak);
        }

     
        ////////////////////////////////////////////////DELETE/////////////////////////////////////////////////

        [Route("ObrisiPlocu/{id}")] //
        [HttpDelete]
        public async Task<ActionResult> ObrisiPlocu(int id)
        {
            
            var vinyl = await Context.Ploce
            .Where(p => p.ID == id)
            .FirstOrDefaultAsync();

            if (vinyl == null) 
            
                return BadRequest("Ploca ne postoji.");
                
            try
            {
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

        // [Route("PronadjiPlocu/{naziv}/{izvodjac}")] 
        // [HttpGet]
        // public async Task<ActionResult> PronadjiPlocu(string naziv, string izvodjac)
        // {
            
        //         var vinyl = await Context.Ploce
        //         .Where(p => p.Ime == naziv)
        //         .FirstOrDefaultAsync();

        //         var iz = await Context.Izvodjaci
        //         .Where(p => p.Ime == izvodjac)
        //         .FirstOrDefaultAsync();

        //         if (vinyl == null) 
                
        //             return BadRequest("Ploca ne postoji.");

        //         if(izvodjac==null)

        //             return BadRequest("Izvodjac ne postoji.");

        //         var pp = await Context.ProdavnicaPloca
        //         .Where(p => p.ploca == vinyl)
        //         .ToListAsync();

        //         try
        //         {
        //         var prod = await Context.ProdavnicaPloca
        //         .Where(p => p.ploca == vinyl)
        //         .ToListAsync();
        //         return Ok(prod.ploca.Select(q =>
        //         new
        //         {
        //             Cena=q.Cena,
        //             Kolicina=q.Kolicina,
        //             Ploca=q.ploca,
        //             Izvodjac=q.ploca.izvodjac,
        //             Zanr=q.ploca.Zanr
                    

        //         }
        //         )
        //         );
        //         }
            
        //     catch (Exception e)
        //         {
        //         return BadRequest(e.Message);
        //         }
        // }
        }

    }





