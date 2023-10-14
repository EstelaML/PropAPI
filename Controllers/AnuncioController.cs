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

        [HttpGet("id")]
        public string GetAnuncioById(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Anuncio.Where(u => u.Id == id).Include(a => a.Comercio).ToList().First();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("novedad/{ID}")]
        public string GetNovedadFromComercio(int ID)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Anuncio.Where(u => u.IdComercio == ID && u.Tipo.Equals("novedad")).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("oferta/{ID}")]
        public string GetOfertaFromComercio(int ID)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Anuncio.Where(u => u.IdComercio == ID && u.Tipo.Equals("oferta")).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("oferta/ultima/{ID}")]
        public string GetLastOfertaFromComercio(int ID)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Anuncio.Where(u => u.IdComercio == ID && u.Tipo.Equals("oferta")).OrderByDescending(u => u.Fecha).ToList().First();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("novedad/ultima/{ID}")]
        public string GetLastNovedadFromComercio(int ID)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Anuncio.Where(u => u.IdComercio == ID && u.Tipo.Equals("novedad")).OrderByDescending(u => u.Fecha).ToList().First();
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
