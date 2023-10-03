using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class UsuarioController
    {
        [HttpGet]
        public String Get()
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Usuario.Include(u => u.IdComercio).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }

        }

        [HttpPost]
        public void Post([FromBody] Usuario usuario)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Usuario.AddAsync(usuario);
                ctx.SaveChanges();
            }
        }

        [HttpPut("{ID}")]
        public void Put(int id, [FromBody] Usuario usuario)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                usuario.Id = id;
                ctx.Usuario.Update(usuario);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("{ID}")]
        public void Delete(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Usuario u = ctx.Usuario.Where(u => u.Id == id).First();
                ctx.Usuario.Remove(u);
                ctx.SaveChanges();
            }
        }
    }
}
