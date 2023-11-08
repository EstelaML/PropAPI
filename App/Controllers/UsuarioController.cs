﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;
using Microsoft.AspNetCore.Cors;
using System.Reflection.Metadata.Ecma335;

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
                var usuarios = ctx.usuario.Include(u => u.idcomercio).Include(u => u.idseguidor).Include(u => u.idseguido).ToList();
                var usuariosProyectados = usuarios.Select(u => new
                {
                    u.id,
                    u.nombre,
                    u.nickname,
                    u.telefono,
                    u.nombreimagen,
                    u.contraseña,
                    u.mail,
                    u.estado,
                    idcomercio = u.idcomercio.Select(c => new
                    {
                        c.id,
                        c.nombre,
                        c.direccion,
                        c.telefono,
                        c.horario,
                        c.web,
                        c.descripcion,
                        c.nombreimagen,
                        c.provincia,
                        c.contraseña,
                        c.mail,
                        c.instagram,
                        c.facebook,
                        c.latitud,
                        c.longitud
                    }),
                    idseguido = u.idseguido.Select(s => new
                    {
                        s.id,
                        s.nombre,
                        s.nickname,
                        s.telefono,
                        s.nombreimagen,
                        s.contraseña,
                        s.mail,
                        s.estado
                    }),
                    idseguidor = u.idseguidor.Select(s => new
                    {
                        s.id,
                        s.nombre,
                        s.nickname,
                        s.telefono,
                        s.nombreimagen,
                        s.contraseña,
                        s.mail,
                        s.estado
                    })
                }).ToList();

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };

                return JsonSerializer.Serialize(usuariosProyectados, options);
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
        public string LoginUsuario(string userCredentials, string contrasena)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var usuario = ctx.usuario.Where(u => (u.nickname == userCredentials || u.mail == userCredentials) && u.contraseña == contrasena).Include(c => c.idcomercio).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return usuario != null ? JsonSerializer.Serialize(usuario, options) : "Credenciales Incorrectas";
            }
        }


        [HttpGet("/Registro/{nickname}/{correo}")]
        public string ComprobacionCredencialesRegistro(string nickname, string correo)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var nombreRepetido = ctx.usuario.Where(x => x.nickname == nickname).ToList();
                var correoRepetido = ctx.usuario.Where(x => x.mail == correo).ToList();
                if (nombreRepetido.Count != 0 && correoRepetido.Count != 0)
                {
                    return "NC";
                } else if (nombreRepetido.Count != 0)
                {
                    return "N";
                } else if (correoRepetido.Count != 0)
                {
                    return "C";
                }
                return "";
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
