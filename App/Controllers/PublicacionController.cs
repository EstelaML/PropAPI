using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.Identity.Client;
using Microsoft.Extensions.Azure;

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/Publicacion")]
    public class PublicacionController : ControllerBase
    {
        [HttpGet]
        public String Get()
        {

            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.publicacion.Include(a => a.comercioObject).Include(l => l.usuarioObject).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }

        }

        [HttpGet("id/{id}")]
        public string GetByPublicacionId(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.publicacion.Where(x => x.id == id)?.Select(c => new
                {
                    c.id,
                    c.descripcion,
                    c.nombreimagen,
                    c.titulo,
                    c.fecha,
                    c.usuarioObject,
                    c.comercioObject
                }).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("usuario/{id}")]
        public string GetUserPublicacionById(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.publicacion.Where(x => x.usuario == id)?.OrderByDescending(c => c.fecha).Select(c => new
                {
                    c.id,
                    c.descripcion,
                    c.nombreimagen,
                    c.titulo,
                    c.fecha,
                    c.usuarioObject,
                    c.comercioObject
                }).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("usuarios")]
        public string GetPublicacionesByUserIds([FromQuery] List<int> ids)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var publicaciones = ctx.publicacion
                    .Where(x => ids.Contains(x.usuario))
                    .OrderByDescending(c => c.fecha)
                    .Select(c => new
                    {
                        PublicacionId = c.id,
                        c.descripcion,
                        nombreimagenpublicacion = c.nombreimagenpublicacion,
                        c.titulo,
                        c.fecha,
                        UsuarioId = c.usuarioObject.id,
                        c.usuarioObject.nombre,
                        c.usuarioObject.nickname,
                        nombreimagenusuario =  c.usuarioObject.nombreimagen,
                        c.comercioObject
                    })
                    .ToList();

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(publicaciones, options);
            }
        }


        [HttpPost]
        public void Post([FromBody] Publicacion publicacion)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.publicacion.AddAsync(publicacion);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("id/{id}")]
        public void DeleteById(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Publicacion a = ctx.publicacion.Where(u => u.id == id).First();
                ctx.publicacion.Remove(a);
                ctx.SaveChanges();
            }
        }

    }

}
