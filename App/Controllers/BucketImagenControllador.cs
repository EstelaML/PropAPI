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
using System;

namespace PropAPI.Controllers
{
    [ApiController]
    [Route("api/Bucket")]
    public class BucketImagenControllador : ControllerBase
    {
        [HttpPost("{bucket}/{name}")]
        public async Task PostAsync(string bucket, string name, [FromBody] string b64)
        {            
            using (PropBDContext ctx = new PropBDContext())
            {
                Supabase.Client client = await ctx.getSupabaseClientAsync();
                var binaryData = Convert.FromBase64String(b64);

                await client.Storage
                    .From("Images/"+ bucket)
                    .Upload(binaryData, name, new Supabase.Storage.FileOptions { CacheControl="3600", Upsert = false });
                ctx.SaveChanges();
            }
        }
    }
}
