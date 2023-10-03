using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/Usuario")]
    public class UsuarioController
    {
        [HttpGet]
        public IEnumerable<dynamic> Get()
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.Usuario.Include(u => u.IdComercio).ToList();
                return l;
            }
        }
    }
}
