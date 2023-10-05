using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;

using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Azure;
using System.IO;
using Microsoft.AspNetCore.Cors;

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/Imagen")]
    [EnableCors("_myAllowSpecificOrigins")]
    public class ImagenController : ControllerBase
    {
        [HttpGet("{nombreImagen}")]
        public async Task<IActionResult> GetImagen(string nombreImagen)
        {
            try
            {
                // Crear un cliente BlobServiceClient
                BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=storageprop;AccountKey=9KA/FW5ozB1VLLg3ZorWfmcOd0qbzO3HLeVksK5PGyWcN8JUzWdVNJtU/Vb6DQT52tbASAcxJTy6+AStAtcc5g==;EndpointSuffix=core.windows.net");


                // Nombre del contenedor donde se encuentra la imagen
                string containerName = "imagenes"; // Reemplaza con el nombre de tu contenedor

                // Crear un cliente BlobContainerClient
                BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

                // Crear un cliente BlobClient para la imagen específica
                BlobClient blobClient = containerClient.GetBlobClient(nombreImagen);

                BlobDownloadInfo blobDownloadInfo = await blobClient.DownloadAsync();

                using (Stream stream = blobDownloadInfo.Content)
                {
                    // Devolver la imagen como una respuesta HTTP
                    if (stream != null)
                    {
                        return File(stream, blobDownloadInfo.ContentType);
                    }
                }

                return NotFound(); // Si la imagen no se encuentra
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al descargar la imagen: {ex.Message}");
                return StatusCode(500); // Maneja el error de manera adecuada en tu aplicación
            }
        }

        [HttpPost("Usuario/{nombreImagen}/{nombreUsuario}")]
        public async Task<IActionResult> PostUserImage(string nombreImagen, string nombreUsuario, [FromBody] string base64)
        {
            var base64Parts = base64.Split(',');
            string encodedData = base64Parts.Length > 1 ? base64Parts[1] : base64;
            byte[] decodedBytes = Convert.FromBase64String(encodedData);

            if (decodedBytes != null)
            {
                using (MemoryStream streamImage = new MemoryStream(decodedBytes))
                {
                    try
                    {
                        BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=storageprop;AccountKey=9KA/FW5ozB1VLLg3ZorWfmcOd0qbzO3HLeVksK5PGyWcN8JUzWdVNJtU/Vb6DQT52tbASAcxJTy6+AStAtcc5g==;EndpointSuffix=core.windows.net");

                        var blobContainerClient = blobServiceClient.GetBlobContainerClient("imagenes");

                        blobContainerClient.CreateIfNotExists();

                        var blobClient = blobContainerClient.GetBlobClient(nombreImagen);

                        await blobClient.UploadAsync(streamImage, true);

                        using (PropBDContext ctx = new PropBDContext())
                        {
                            Usuario user = ctx.Usuario.Where(u => u.NickName == nombreUsuario).First();
                            user.ImagenName = nombreImagen;
                            ctx.SaveChanges();
                            return Ok("¡Operación exitosa!");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error al subir la imagen: {e.Message}");
                        return StatusCode(500); // Maneja el error de manera adecuada en tu aplicación
                    }
                }
            }
            return StatusCode(500); // Maneja el error de manera adecuada en tu aplicación
        }


        [HttpPost("Comercio/{nombreImagen}/{idComercio}")]
        public async Task<IActionResult> PostCompanyImage(string nombreImagen, int idComercio, [FromBody] string base64)
        {
            var base64Parts = base64.Split(',');
            string encodedData = base64Parts.Length > 1 ? base64Parts[1] : base64;
            byte[] decodedBytes = Convert.FromBase64String(encodedData);

            if (decodedBytes != null)
            {
                using (MemoryStream streamImage = new MemoryStream(decodedBytes))
                {
                    try
                    {
                        BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=storageprop;AccountKey=9KA/FW5ozB1VLLg3ZorWfmcOd0qbzO3HLeVksK5PGyWcN8JUzWdVNJtU/Vb6DQT52tbASAcxJTy6+AStAtcc5g==;EndpointSuffix=core.windows.net");

                        var blobContainerClient = blobServiceClient.GetBlobContainerClient("imagenes");

                        blobContainerClient.CreateIfNotExists();

                        var blobClient = blobContainerClient.GetBlobClient(nombreImagen);

                        await blobClient.UploadAsync(streamImage, true);

                        using (PropBDContext ctx = new PropBDContext())
                        {
                            Comercio comercio = ctx.Comercio.Where(u => u.Id == idComercio).First();
                            comercio.ImagenNombre = nombreImagen;
                            ctx.SaveChanges();
                            return Ok("¡Operación exitosa!");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Error al subir la imagen: {e.Message}");
                        return StatusCode(500); // Maneja el error de manera adecuada en tu aplicación
                    }
                }
            }
            return StatusCode(500); // Maneja el error de manera adecuada en tu aplicación
        }
    }
}
