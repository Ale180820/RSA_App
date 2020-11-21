using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CustomGenerics.Encryption;
using RSAApp.Models;
using Microsoft.AspNetCore.Hosting;
using System.IO.Compression;
using System.Net.Http;
using System.Net;

namespace RSAApp.Controllers {

    [ApiController]
    public class RSAController : ControllerBase {


        public Route route = new Route();

        public RSAController(IWebHostEnvironment env){
            route.hostEnvironment = env;
        }

      
        [HttpGet]
        [Route("api/rsa/keys/{p}/{q}")]
        public async Task<IActionResult> GetFile(int p, int q) {
            RSA rsa = new RSA();
            
            if (string.IsNullOrWhiteSpace(route.webRoot())) {
                route.hostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "RSADocs");
            }

            if (!Directory.Exists(route.setKDirectory())) {
                Directory.CreateDirectory(route.setKDirectory());
            }
            while (System.IO.File.Exists(route.getKeysZip())){
                System.IO.File.Delete(route.getKeysZip());
            }
            rsa.calculateKey(p, q, route.getPrivateKeyR(), route.getPublicKeyR());
            //Add keys in zip
            var zip = ZipFile.Open(route.getKeysZip(), ZipArchiveMode.Create);
            zip.CreateEntryFromFile(route.getPublicKeyR(), "public.key", CompressionLevel.Optimal);
            zip.CreateEntryFromFile(route.getPrivateKeyR(), "private.key", CompressionLevel.Optimal);
            zip.Dispose();

            System.IO.File.Delete(route.getPrivateKeyR());
            System.IO.File.Delete(route.getPublicKeyR());

            var fileStream = new FileStream(route.getKeysZip(), FileMode.OpenOrCreate);
            return File(fileStream, "application/zip");
            
        }
        

        //POST - Encrypt or dencrypt the file
        [HttpPost]
        [Route("api/rsa/{nombre}")]
        public async Task<IActionResult> EncryptOrDencrypt([FromForm] IFormFile file, [FromForm] IFormFile key, string nombre) {
            RSA rsa = new RSA();
            if (string.IsNullOrWhiteSpace(route.webRoot())) {
                route.hostEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "RSADocs");
            }

            if (!Directory.Exists(route.setCDirectory())) {
                Directory.CreateDirectory(route.setCDirectory()); 
            }

            string option = ""; 
            if (key.FileName == "public.key") {  option = "e"; }
            else {  option = "d";  }
            
            var path = route.setRoute(file.FileName);
            var secondPath = route.setNewRoute(nombre);

            //Read file's keys
            var keysF = route.setKeysRoute(key.FileName);
            using var fileKeys = System.IO.File.Create(keysF);
            key.CopyTo(fileKeys);
            fileKeys.Flush();
            fileKeys.Close();

            //Encryption or Dencryption file
            using var fileCreate = System.IO.File.Create(path);
            file.CopyTo(fileCreate);
            fileCreate.Flush();
            fileCreate.Close();
            string nameFile = file.FileName.Split(".")[0];

            //RSA encryption or Dencryption
            rsa.encryptOrDencrypt(keysF, path, secondPath, option);

            System.IO.File.Delete(path);
            System.IO.File.Delete(keysF);

            var memoryS = new MemoryStream();

            using (var fileStream = new FileStream(secondPath, FileMode.Open)) {
                await fileStream.CopyToAsync(memoryS);
            }
            memoryS.Position = 0;
            return File(memoryS, System.Net.Mime.MediaTypeNames.Application.Octet, nombre + ".txt");
        }
    }
}
