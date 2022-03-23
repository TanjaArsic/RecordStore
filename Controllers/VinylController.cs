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
    

        [Route("IzmeniCenuPloce/{naziv}/{izvodjac}/{cena}")] //dobaaaaaaaaaaaar
        [HttpPut]
        public async Task<ActionResult> IzmeniPlocu(string naziv, string izvodjac, int cena)
        {
            try
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
        [Route("SmanjiKolicinuPloce/{naziv}/{izvodjac}")] //dobaaaaaaaaaaaar
        [HttpPut]
        public async Task<ActionResult> SmanjiKolicinuPlocu(string naziv, string izvodjac)
        {
            try
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



