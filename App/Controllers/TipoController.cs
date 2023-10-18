using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;
using Microsoft.AspNetCore.Cors;

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/Tipo")]
    [EnableCors("_myAllowSpecificOrigins")]
    public class TipoController
    {
        [HttpGet]
        public String Get()
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var tipo = ctx.tipo;
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(tipo, options);
            }

        }

        [HttpPost]
        public  void Post([FromBody] Tipo tipo)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.tipo.AddAsync(tipo);
                ctx.SaveChanges();
            }
        }

        [HttpPut("{ID}")]
        public void Put(int id, [FromBody] Tipo tipo)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                tipo.id = id;
                ctx.tipo.Update(tipo);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("{ID}")]
        public void Delete(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Tipo tipo = ctx.tipo.Where(u => u.id == id).First();
                ctx.tipo.Remove(tipo);
                ctx.SaveChanges();
            }
        }
    }
}
