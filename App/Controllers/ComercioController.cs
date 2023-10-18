﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using WebApplication1.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.Identity.Client;

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
                var l = ctx.comercio.Include(c => c.idusuario).Include(c => c.tipo_id).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }

        }

        [HttpGet("/api/Comercio/nombre/{nombreComercio}")]
        public string GetComercioByName(string nombreComercio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.comercio.Where(u => u.nombre == nombreComercio).Include(c => c.idusuario).Include(c => c.tipo_id).ToList().First();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("/api/Comercio/string/{nombreComercio}")]
        public string GetComercioByStringName(string nombreComercio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.comercio.Where(u => u.nombre.Contains(nombreComercio)).ToList();
                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                };
                return JsonSerializer.Serialize(l, options);
            }
        }

        [HttpGet("{ID}")]
        public string GetComercioById(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                var l = ctx.comercio.Where(u => u.id == id).Include(c => c.idusuario).Include(c => c.tipo_id).ToList().First();
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
                var l = ctx.comercio.AddAsync(comercio);
                ctx.SaveChanges();
            }
        }

        [HttpPut("{ID}")]
        public void Put(int id, [FromBody] Comercio comercio)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                comercio.id = id;
                ctx.comercio.Update(comercio);
                ctx.SaveChanges();
            }
        }

        [HttpDelete("{ID}")]
        public void Delete(int id)
        {
            using (PropBDContext ctx = new PropBDContext())
            {
                Comercio c = ctx.comercio.Where(u => u.id == id).First();
                ctx.comercio.Remove(c);
                ctx.SaveChanges();
            }
        }
    }

}
