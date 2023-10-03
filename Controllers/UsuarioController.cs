using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;

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
                var l = ctx.Usuario.Include(u => u.IdComercio).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    // Otros ajustes opcionales
                };

                return JsonSerializer.Serialize(l, options);
            }

        }
    }
}
