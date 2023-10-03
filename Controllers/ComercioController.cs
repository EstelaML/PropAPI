using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;

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
                    var l = ctx.Comercio.Include(c => c.IdUsuario).Include(c => c.Tipo).ToList();
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
                    var l = ctx.Comercio.AddAsync(comercio);
                    ctx.SaveChanges();
                }
            }

            [HttpPut("{ID}")]
            public void Put(int id, [FromBody] Comercio comercio)
            {
                using (PropBDContext ctx = new PropBDContext())
                {
                    comercio.Id = id;
                    ctx.Comercio.Update(comercio);
                    ctx.SaveChanges();
                }
            }

            [HttpDelete("{ID}")]
            public void Delete(int id)
            {
                using (PropBDContext ctx = new PropBDContext())
                {
                    Comercio c = ctx.Comercio.Where(u => u.Id == id).First();
                    ctx.Comercio.Remove(c);
                    ctx.SaveChanges();
                }
            }
        }
    
}
