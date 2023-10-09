using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;
using Microsoft.AspNetCore.Cors;

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

        [HttpGet("{ID}")]
        public string GetUsuarioById(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Usuario.Where(u => u.Id == id).Include(c => c.IdComercio).ToList().First();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("/api/Usuario/name/{nombreUsuario}")]
        public string GetUsuarioByName(string nombreUsuario)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Usuario.Where(u => u.Nombre == nombreUsuario).Include(c => c.IdComercio).ToList().First();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("/api/Usuario/string/{nombreUsuario}")]
        public string GetUsuarioByStringName(string nombreUsuario)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Usuario.Where(u => u.NickName.Contains(nombreUsuario)).Include(c => c.IdComercio).ToList().First();
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

        [HttpPut("/{id}")]
        public void Put(int id, [FromBody] Usuario usuario)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                usuario.Id = id;
                ctx.Usuario.Update(usuario);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("/{id}")]
        public void Delete(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Usuario u = ctx.Usuario.Where(u => u.Id == id).First();
                ctx.Usuario.Remove(u);
                ctx.SaveChanges();
            }
        }

        //[HttpGet("PorNombre")]
        //public String GetUsuariosPorNombreE()
        //{
        //    using (PropBDContext ctx = new PropBDContext())
        //    {
        //        var usuariosConA = ctx.Usuario
        //            .AsEnumerable()
        //            .Where(u => u.Nombre.StartsWith("E", StringComparison.OrdinalIgnoreCase))
        //            .ToList();

        //        var options = new JsonSerializerOptions
        //        {
        //            ReferenceHandler = ReferenceHandler.Preserve,
        //        };

        //        return JsonSerializer.Serialize(usuariosConA, options);
        //    }
        //}

    }
}
