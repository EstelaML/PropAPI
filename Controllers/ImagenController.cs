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

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/Imagen")]
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

        [HttpPost("{nombreImagen}")]
        public void PostImage(string nombreImagen, [FromBody] string base64)
        {
            var base64Parts = base64.Split(',');
            string encodedData = base64Parts.Length > 1 ? base64Parts[1] : base64;

            byte[] decodedBytes = Convert.FromBase64String(encodedData);

            using (MemoryStream streamImage = new MemoryStream(decodedBytes))
            {
                try {
                    BlobServiceClient blobServiceClient = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=storageprop;AccountKey=9KA/FW5ozB1VLLg3ZorWfmcOd0qbzO3HLeVksK5PGyWcN8JUzWdVNJtU/Vb6DQT52tbASAcxJTy6+AStAtcc5g==;EndpointSuffix=core.windows.net");

                    var blobContainerClient = blobServiceClient.GetBlobContainerClient("imagenes");

                    blobContainerClient.CreateIfNotExists();

                    var blobClient = blobContainerClient.GetBlobClient(nombreImagen);

                    blobClient.Upload(streamImage, true);
                } catch (Exception e)
                {

                }
            }
        }
    }
}
