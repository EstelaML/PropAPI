using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;

using static System.Net.Mime.MediaTypeNames;

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/Imagen")]
    public class ImagenController
    {
        [HttpGet]
        public String Get()
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Imagen.Include(u => u.IdNavigation).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpPost]
        public void Post([FromBody] String path)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                byte[] byteArray = File.ReadAllBytes(path);
                Console.WriteLine(byteArray.Length);
                Usuario a = ctx.Usuario.ToList().First();

                Imagen i = new Imagen
                {
                    ImagenGrande = byteArray,
                    ImagenReducidad = null,
                    Tipo = "perfil",
                };
                var imagen = ctx.Imagen.Add(i).Entity;

                a.Imagen = imagen;

                ctx.SaveChanges();
            }
        }


    }
}
