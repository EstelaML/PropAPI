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
    [Route("api/Lista")]
    public class ListaController
    {
        [HttpGet]
        public String Get()
        {

            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.lista.Include(l => l.usuario).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }

        }

        [HttpGet("id/{id}")]
        public string GetListaById(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.lista.Where(u => u.id == id).Include(a => a.Comercio).ToList().First();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("id/usuario/{id}")]
        public string GetListaByUserId(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var listas = ctx.lista.Where(u => u.idusuario == id).Include(a => a.Comercio).ToList();
                var l = listas.Select(l => new
                {
                    l.id,
                    l.idusuario,
                    l.nombre,
                    Comercio = l.Comercio.Select(c => new
                    {
                        c.id,
                        c.nombre,
                    })
                }).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("id/usuario/sololistas/{id}")]
        public string GetListaByUserIdListas(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.lista.Where(u => u.idusuario == id).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("id/usuario/recomendedListas/{id}")]
        public string GetRecomendedListaByUserId(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var listas = ctx.lista.Where(l => l.idusuario != id && !l.usuarioSeguidos.Any(u => u.id == id)).Include(l => l.usuario).ToList();
            

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(listas, options);
            }
        }

        [HttpPost]
        public void Post([FromBody] Lista lista)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.lista.AddAsync(lista);
                ctx.SaveChanges();
            }
        }

        [HttpPost("lista/comercio/{idlista}/{idcomercio}")]
        public void Post(int idlista, int idcomercio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var lista = ctx.lista.Where(l => l.id == idlista).First();
                var comercio = ctx.comercio.Where(c => c.id == idcomercio).First();

                if (lista != null && comercio != null)
                {
                    lista.Comercio.Add(comercio);
                    ctx.SaveChanges();
                }

            }
        }

        [HttpPut("{ID}")]
        public void Put(int id, [FromBody] Lista lista)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                lista.id = id;
                ctx.lista.Update(lista);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("{ID}")]
        public void Delete(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Lista a = ctx.lista.Where(u => u.id == id).First();
                ctx.lista.Remove(a);
                ctx.SaveChanges();
            }
        }

        /*[HttpDelete("borrarNombre/{nombre}")]
        public void BorrarPorNombre(string nombre)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var lista = ctx.lista.Where(l => l.nombre == nombre).First();
                List<Comercio> comercios = (List<Comercio>)lista.Comercio;

                if (lista != null && comercios != null)
                {
                    lista.Comercio.Clear();
                    ctx.SaveChanges();
                    foreach (var comercio in comercios)
                    {
                        comercio.lista_id.Remove(lista);
                    }
                    ctx.lista.Remove(lista);
                    ctx.SaveChanges();
                }

            }
        }*/

        [HttpDelete("borrarNombre/{nombre}")]
        public void BorrarPorNombre(string nombre)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var lista = ctx.lista.Where(l => l.nombre == nombre).First();
                List<Comercio> comercios = (List<Comercio>)lista.Comercio;
                ctx.lista.Remove(lista);
                ctx.SaveChanges();

            }
        }
    }

}
