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
    [Route("api/Comercio")]
    public class ComercioController
    {
        [HttpGet]
        public String Get()
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var comercios = ctx.comercio.Include(c => c.idusuario).Include(c => c.tipo_id).ToList();
                var comerciosProyectados = comercios.Select(c => new
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
                    c.longitud,
                    c.valoracionpromedio,
                    idusuario = c.idusuario.Select(u => new
                    {
                        u.id,
                        u.nombre,
                        u.nickname,
                        u.telefono,
                        u.nombreimagen,
                        u.contraseña,
                        u.mail,
                        u.estado
                    }),
                    tipo_id = c.tipo_id.Select(t => new
                    {
                        t.id,
                        t.nombre,
                        t.descripcion
                    })
                }).ToList();

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };

                return JsonSerializer.Serialize(comerciosProyectados, options);
            }
        }

        [HttpGet("/api/Comercio/filtros/{puntuacion}/{tipo}")]
        public String Filters(int puntuacion, int tipo)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var comerciosFiltrados = ctx.comercio
                    .Where(c => puntuacion == 0 || c.valoracionpromedio >= puntuacion)
                    .Where(c => tipo == 0 || c.tipo_id.Any(t => t.id == tipo))
                    .Select(c => new
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
                        c.longitud,
                        c.valoracionpromedio,
                        idusuario = c.idusuario.Select(u => new
                        {
                            u.id,
                            u.nombre,
                            u.nickname,
                            u.telefono,
                            u.nombreimagen,
                            u.contraseña,
                            u.mail,
                            u.estado
                        }),
                        tipo_id = c.tipo_id.Select(t => new
                        {
                            t.id,
                            t.nombre,
                            t.descripcion
                        })
                    }).ToList();

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };

                return JsonSerializer.Serialize(comerciosFiltrados, options);
            }
        }

        [HttpGet("/api/Comercio/Login")]
        public string LoginComercio(string userCredentials, string contrasena)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var comercio = ctx.comercio.Where(u => (u.nombre == userCredentials || u.mail == userCredentials) && u.contraseña == contrasena).Include(c => c.idusuario).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return comercio != null ? JsonSerializer.Serialize(comercio, options) : "Credenciales Incorrectas";
            }
        }

        [HttpGet("/api/Comercio/mail/{correo}")]
        public bool MailRepetido(string correo)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.comercio.Where(c => c.mail == correo).ToArray().Length;
                return l != 0;
            }

        }


        [HttpGet("/api/Comercio/nombre/{nombreComercio}")]
        public string GetComercioByName(string nombreComercio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.comercio.Where(u => u.nombre == nombreComercio).Include(c => c.idusuario).Include(c => c.tipo_id).ToList().First();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("/api/Comercio/email")]
        public string GetComercioByEmail(string email)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.comercio.Where(u => u.mail == email).Include(c => c.idusuario).Include(c => c.tipo_id).ToList().First();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("/api/Comercio/string/{nombreComercio}")]
        public string GetComercioByStringName(string nombreComercio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.comercio.Where(u => u.nombre.Contains(nombreComercio)).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("{ID}")]
        public string GetComercioById(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.comercio.Where(u => u.id == id).Include(c => c.idusuario).Include(c => c.tipo_id).ToList().First();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpPost]
        public void Post([FromBody] Comercio comercio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.comercio.AddAsync(comercio);
                ctx.SaveChanges();
            }
        }

        [HttpPut("{ID}")]
        public void Put(int id, [FromBody] Comercio comercio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                comercio.id = id;
                ctx.comercio.Update(comercio);
                ctx.SaveChanges();
            }
        }

        [HttpPut("/api/Comercio/editarNombreImagen/{id}/{nombreImagen}")]
        public void editarNombreImagen(int id, string nombreImagen)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var comercio = ctx.comercio.FirstOrDefault(u => u.id == id);
                if (comercio != null)
                {
                    comercio.nombreimagen = nombreImagen;
                    ctx.comercio.Update(comercio);
                    ctx.SaveChanges();
                }
            }
        }

        [HttpDelete("{ID}")]
        public void Delete(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Comercio c = ctx.comercio.Where(u => u.id == id).First();
                ctx.comercio.Remove(c);
                ctx.SaveChanges();
            }
        }
    }



}
