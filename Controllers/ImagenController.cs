using Azure;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}
