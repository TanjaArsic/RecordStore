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
    public class IzvodjacController : ControllerBase
    {
        public VinylContext Context { get; set; }

        public IzvodjacController(VinylContext context)
        {
            Context = context;
        }

        [Route("PreuzmiIzvodjaca")]
        [HttpGet]
        public async Task<ActionResult> Preuzmi()
        {
            try
            {
                return Ok(await Context.Izvodjaci.Select(p => new { p.ID, p.Ime}).ToListAsync());
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DodajIzvodjaca")]
        [HttpPost]
        public async Task<ActionResult> DodajIzvodjaca([FromBody] Izvodjac izvodjac)
        {
            if (string.IsNullOrWhiteSpace(izvodjac.Ime) || izvodjac.Ime.Length > 50)
            {
                return BadRequest("Lose ime izvodjaca");
            }

            try
            {
                Context.Izvodjaci.Add(izvodjac);
                await Context.SaveChangesAsync();
                return Ok($"Izvodjac je dodat! ID je: {izvodjac.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("PromenitiIzvodjaca")]
        [HttpPut]
        public async Task<ActionResult> PromenitiIzvodjaca([FromBody] Izvodjac izvodjac)
        {
            if (izvodjac.ID <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }

            if (izvodjac.Ime.Length > 100)
            {
                return BadRequest("Lose ime izvodjaca");
            }


            try
            {
                Context.Izvodjaci.Update(izvodjac);

                await Context.SaveChangesAsync();
                return Ok($"Uspešno izmenjen izvodjac! ID je: {izvodjac.ID}");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("IzbrisiIzvodjaca")]
        [HttpDelete]
        public async Task<ActionResult> IzbrisatiIzvodjaca(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Pogrešan ID!");
            }

            try
            {
                var izvodjac = await Context.Izvodjaci.FindAsync(id);
                Context.Izvodjaci.Remove(izvodjac);
                await Context.SaveChangesAsync();
                return Ok($"Uspešno izbrisan izvodjac {izvodjac.Ime} ");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
