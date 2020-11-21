using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RSAApp.Models
{
    public class Route
    {
        public Route() { }

        public IWebHostEnvironment hostEnvironment { get; set; }

        public string webRoot() {
            return hostEnvironment.WebRootPath;
        }

        public string setCDirectory() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\";
        }

        public string setRoute(string fileName) {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + fileName;
        }
        public string setNewRoute(string nameNewFile) {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + nameNewFile + ".txt";
        }

        public string setKeysRoute(string keyFile) {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + keyFile;
        }


        public string setKDirectory() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\"+"\\Keys\\";
        }

        public string getKeysZip() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + "\\Keys\\" + "ZipFile"+".zip";
        }

        public string getPublicKeyR() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" +"\\Keys\\"+"public.key"+".txt";
        }

        public string getPrivateKeyR() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + "\\Keys\\" +"private.key"+".txt";
        }
    }
}
