using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using WebApplication1.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.Identity.Client;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Drawing;
using System.Net.Http.Headers;
using System.Net;
using System.IO;
using System.Drawing.Imaging;
using Azure.Storage.Blobs.Models;
using System.Collections;

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/Imagenes")]
    public class ImagenControllador : ControllerBase
    {
        [HttpGet("api/Imagenes/{id}")]
        public IActionResult GetById(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var imageEntity = ctx.imagenes.FirstOrDefault(e => e.id == id);

                if (imageEntity == null || imageEntity.image == null || imageEntity.image.Length == 0)
                {
                    return StatusCode(404);
                }
                string contentType = "image/*";
                MemoryStream stream = new MemoryStream(imageEntity.image);
                return File(stream, contentType);
            }
        }

        [HttpGet("api/Imagenes/nombre/{nombre}")]
        public IActionResult GetByNombre(string nombre)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var imageEntity = ctx.imagenes.FirstOrDefault(e => e.nombre == nombre);

                if (imageEntity == null || imageEntity.image == null || imageEntity.image.Length == 0)
                {
                    return StatusCode(404);
                }
                string contentType = "image/*";
                MemoryStream stream = new MemoryStream(imageEntity.image);
                return File(stream, contentType);
            }
        }

        [HttpPost("{nombreImagen}")]
        public void Post(string nombreImagen, [FromBody] string base64)
        {
            byte[] byteArray = Convert.FromBase64String(base64);

            Imagen i = new Imagen();
            i.image = byteArray;
            i.nombre = nombreImagen;

            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.imagenes.AddAsync(i);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("{ID}")]
        public void DeleteById(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Imagen a = ctx.imagenes.Where(u => u.id == id).First();
                ctx.imagenes.Remove(a);
                ctx.SaveChanges();
            }
        }
    }

}
