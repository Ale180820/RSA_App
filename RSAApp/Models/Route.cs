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

        #region Directories
        public string setCDirectory() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\";
        }
        public string setCipherDirectory() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + "\\Cipher\\";
        }
        public string setDecipherDirectory() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + "\\Decipher\\";
        }
        #endregion

        #region Cipher routes
        public string setRoute(string fileName) {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + "\\Cipher\\" + fileName;
        }
        public string setNewRoute(string nameNewFile) {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + "\\Cipher\\" + nameNewFile + ".txt";
        }
        #endregion

        #region Decipher routes
        public string setDRoute(string fileName)
        {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + "\\Decipher\\" + fileName;
        }
        public string setNewDRoute(string nameNewFile)
        {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + "\\Decipher\\" + nameNewFile + ".txt";
        }
        #endregion

        #region Keys
        public string setKeysRoute(string keyFile) {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + keyFile;
        }


        public string setKDirectory() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\"+"\\Keys\\";
        }
        #endregion

        #region Zip file
        //Zip file
        public string getKeysZip() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + "\\Keys\\" + "ZipFile"+".zip";
        }

        public string getPublicKeyR() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" +"\\Keys\\"+"public.key"+".txt";
        }

        public string getPrivateKeyR() {
            return hostEnvironment.WebRootPath + "\\RSADocs\\" + "\\Keys\\" +"private.key"+".txt";
        }
        #endregion
    }
}
