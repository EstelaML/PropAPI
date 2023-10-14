using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.Identity.Client;

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/Anuncio")]
    public class AnuncioController
    {
        [HttpGet]
        public String Get()
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Anuncio.Include(a => a.Comercio).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }

        }


        [HttpGet("{ID}")]
        public string GetAnuncioById(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Anuncio.Where(u => u.Id == id).Include(c => c.Comercio).ToList().First();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpPost]
        public void Post([FromBody] Anuncio anuncio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Anuncio.AddAsync(anuncio);
                ctx.SaveChanges();
            }
        }

        [HttpPut("{ID}")]
        public void Put(int id, [FromBody] Anuncio anuncio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                anuncio.Id = id;
                ctx.Anuncio.Update(anuncio);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("{ID}")]
        public void Delete(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Anuncio a = ctx.Anuncio.Where(u => u.Id == id).First();
                ctx.Anuncio.Remove(a);
                ctx.SaveChanges();
            }
        }
    }

}
