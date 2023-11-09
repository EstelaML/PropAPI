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
    [Route("api/Reseña")]
    public class ReseñaController : ControllerBase
    {
        [HttpGet("Comercio/{id}")]
        public string GetByComercioId(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.reseña.Include(a => a.usuarioObject).Where(x => x.comercio == id)?.ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("Usuario/{id}")]
        public string GetByUsuarioId(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.reseña.Include(a => a.comercioObject).Where(x => x.usuario == id).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("{idComercio}/{idUsuario}")]
        public Boolean ExisteUsuarioComercioReseña(int idComercio, int idUsuario)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.reseña.Where(x => x.usuario == idUsuario && x.comercio == idComercio).ToList();
                return l.Count != 0;
                // si diferente de 0 existe y devuelve true
                // si no existe devuelve false porque es 0
            }
        }

        [HttpPost]
        public void Post([FromBody] Reseña reseña)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.reseña.AddAsync(reseña);
                ctx.SaveChanges();
            }
        }

        [HttpPut("{ID}")]
        public void Put(int id, [FromBody] Anuncio anuncio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                anuncio.id = id;
                ctx.anuncio.Update(anuncio);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("Comercio/{id}")]
        public void DeleteByIdComercio(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Reseña a = ctx.reseña.Where(u => u.comercio == id).First();
                ctx.reseña.Remove(a);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("Usuario/{id}")]
        public void DeleteByIdUsuario(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Reseña a = ctx.reseña.Where(u => u.usuario == id).First();
                ctx.reseña.Remove(a);
                ctx.SaveChanges();
            }
        }
    }

}
