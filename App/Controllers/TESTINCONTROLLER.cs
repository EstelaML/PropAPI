using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/loaderio-3bfeb3f079abdd8776a0af0de67b07e1/")]
    public class TestingController : ControllerBase
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TestingController(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet("/")]
        public IActionResult DownloadFile()
        {
            // Ruta relativa al directorio de la aplicación
            string relativePath = Path.Combine("Controllers", "loaderio-3bfeb3f079abdd8776a0af0de67b07e1.txt");

            // Ruta completa del archivo que deseas devolver
            string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, relativePath);

            // Verificar si el archivo existe
            if (System.IO.File.Exists(filePath))
            {
                // Leer el contenido del archivo
                var fileBytes = System.IO.File.ReadAllBytes(filePath);

                // Devolver el archivo como una respuesta HTTP
                return File(fileBytes, "application/octet-stream", "loaderio-3bfeb3f079abdd8776a0af0de67b07e1.txt");
            }
            else
            {
                // El archivo no existe
                return NotFound();
            }
        }
    }
}
