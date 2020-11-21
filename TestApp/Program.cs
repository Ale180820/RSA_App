using System;
using CustomGenerics.Encryption;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            RSA rsa = new RSA();
            string zipRoute = "C:\\Users\\ayalr\\Documents\\prueba\\zipK.zip";
            string publicK = "C:\\Users\\ayalr\\Documents\\prueba\\publicK.txt";
            string privateK = "C:\\Users\\ayalr\\Documents\\prueba\\privateK.txt";
            rsa.calculateKey(89, 97, publicK, privateK);
            string keys = "C:\\Users\\ayalr\\Documents\\prueba\\keys.txt";
            string keyP = "C:\\Users\\ayalr\\Documents\\prueba\\keyp.txt";
            string readPath = "C:\\Users\\ayalr\\Documents\\prueba\\pruebaCifrado.txt";
            string writePath = "C:\\Users\\ayalr\\Documents\\prueba\\cifrado.txt";
            string secondWritePath = "C:\\Users\\ayalr\\Documents\\prueba\\resultado.txt";
            //rsa.encryptOrDencrypt(keyP, readPath, writePath);
            //rsa.encryptOrDencrypt(keys, writePath, secondWritePath);
        }
    }
}
