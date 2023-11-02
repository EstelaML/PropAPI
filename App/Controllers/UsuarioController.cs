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
                var l = ctx.usuario.Include(u => u.idcomercio).ToList();
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
                var l = ctx.usuario.Where(u => u.id == id).Include(c => c.idcomercio).Include(u => u.idseguido).Include(u => u.idseguidor).ToList().First();
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
                var l = ctx.usuario.Where(u => u.nombre == nombreUsuario).Include(c => c.idcomercio).ToList().First();
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
                var l = ctx.usuario.Where(u => u.nickname.Contains(nombreUsuario)).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("/api/Usuario/Login")]
        public bool LoginUsuario(string nombreUsuario, string contrasena)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var usuario = ctx.usuario
                    .FirstOrDefault(u => u.nickname == nombreUsuario && u.contraseña == contrasena);

                return usuario != null ? true : false;
            }
        }




        [HttpPost]
        public void Post([FromBody] Usuario usuario)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.usuario.AddAsync(usuario);
                ctx.SaveChanges();
            }
        }

        [HttpPut("/{id}")]
        public void Put(int id, [FromBody] Usuario usuario)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                usuario.id = id;
                ctx.usuario.Update(usuario);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("/{id}")]
        public void Delete(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Usuario u = ctx.usuario.Where(u => u.id == id).First();
                ctx.usuario.Remove(u);
                ctx.SaveChanges();
            }
        }
        //
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
