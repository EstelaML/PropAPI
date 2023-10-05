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
                var tipo = ctx.Tipo;
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
                var l = ctx.Tipo.AddAsync(tipo);
                ctx.SaveChanges();
            }
        }

        [HttpPut("{ID}")]
        public void Put(int id, [FromBody] Tipo tipo)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                tipo.Id = id;
                ctx.Tipo.Update(tipo);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("{ID}")]
        public void Delete(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Tipo tipo = ctx.Tipo.Where(u => u.Id == id).First();
                ctx.Tipo.Remove(tipo);
                ctx.SaveChanges();
            }
        }
    }
}
