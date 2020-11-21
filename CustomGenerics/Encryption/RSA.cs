using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Numerics;
using System.Text;

namespace CustomGenerics.Encryption {
    public class RSA {
        private int pOp = 0;
        private int n = 0;
        public void encryptOrDencrypt(string keysFile, string path, string writePath, string publicOrPriv) {
            readKeys(keysFile);
            if (publicOrPriv == "e") { Encrypt(path, writePath); }
            else { Dencrypt(path, writePath); }
        }

        #region Encryption
        //Encrypt file
        private void Encrypt(string path, string writePath) {

            //Read file with BinaryReader
            using var file = new FileStream(path, FileMode.Open);
            using var reader = new BinaryReader(file);

            //Write in file cipher file with BinaryWriter
            using var filewrite = new FileStream(writePath, FileMode.Create);
            using var write = new BinaryWriter(filewrite);

            var buffer = new byte[1000000];
            var listBytes = new List<byte>();
            while (reader.BaseStream.Position != reader.BaseStream.Length) {
                buffer = reader.ReadBytes(1000000);
                foreach (var item in buffer) {
                    BigInteger value = BigInteger.ModPow(item, pOp, n);
                    byte[] bytes = BitConverter.GetBytes((long)value);
                    foreach (var pos in bytes){
                        listBytes.Add(pos);
                    }
                }
                write.Write(listBytes.ToArray());
                listBytes.Clear();
            }
            write.Close();
            reader.Close();
        }
        #endregion

        #region Dencryption
        //Dencrypt file
        private void Dencrypt(string path, string writePath) {

            //Read cipher file with BinaryReadear
            using var file = new FileStream(path, FileMode.Open);
            using var reader = new BinaryReader(file);

            //Write in file with BinaryWriter
            using var filewrite = new FileStream(writePath, FileMode.Create);
            using var write = new BinaryWriter(filewrite);

            var buffer = new byte[1000000];
            var listBytes = new List<byte>();
            var listDefineBytes = new List<byte>();
            while (reader.BaseStream.Position != reader.BaseStream.Length) {
                buffer = reader.ReadBytes(1000000);
                foreach (var item in buffer) {
                    listDefineBytes.Add(item);
                    //Add 8 bits in list
                    if (listDefineBytes.Count == 8) {
                        int bits = BitConverter.ToInt32(listDefineBytes.ToArray());
                        BigInteger modPow = BigInteger.ModPow(bits, pOp, n);
                        byte Byte = Convert.ToByte((long)modPow);
                        listBytes.Add(Byte);
                        listDefineBytes.Clear();
                    }
                }
                write.Write(listBytes.ToArray());
                listBytes.Clear();
            }
            write.Close();
            reader.Close();
        }
        #endregion

        #region Read keys
        private void readKeys(string path) {
            string text = "";
            using var file = new StreamReader(path);
            while ((text = file.ReadLine()) != null) {
                n = int.Parse(text.Split(',')[0]);
                pOp = int.Parse(text.Split(',')[1]);
            }
        }
        #endregion

        #region Generate Keys
        public void calculateKey(int p, int q, string privateK, string publicK) { 
            var n = p * q;
            var phi = (p - 1) * (q - 1);
            var e = findEVal(p, q, phi);
            var d = findD(phi, e);
            writeKeys(n, e, d, publicK, privateK); 
        }
        //Find "e" value
        private int findEVal(int p, int q, int phi) { 
            //Find phi factors
            var e = 0;
            int con = 2;
            bool findV = false;
            while (findV == false && con < phi) {
                if (isPrime(con) && phi % con != 0) {
                    e = con;
                    return e;
                }
                con++;
            }
            return 0;
        }

        private bool isPrime(int n) {
            int m = n / 2;
            for (int i = 2; i <= m; i++) {
                if (n % i == 0) {
                    return false;
                }
            }
            return true;
        }

        //Find "d" value
        private int findD(int phi, int e) {
            List<int[]> dValue = new List<int[]>();
            int[] reference = new int[2];
            reference[0] = phi;
            reference[1] = phi;
            dValue.Add(reference);
            reference = new int[2];
            reference[0] = e;
            reference[1] = 1;
            dValue.Add(reference);
            int counter = 0;
            int beforeVL;
            int beforeVL2;
            int beforeVR;
            int beforeVR2;
            while (dValue[dValue.Count - 1][0] != 1) {
                int[] result = new int[2];
                beforeVL = dValue[counter][0];
                beforeVL2 = dValue[counter+1][0];
                beforeVR = dValue[counter][1];
                beforeVR2 = dValue[counter+1][1];
                int x = (beforeVL / beforeVL2);
                int resultL = beforeVL - (x * beforeVL2);
                int resultR = beforeVR - (x * beforeVR2);
                if (resultR < 0) {
                    resultR = resultR % phi;
                }
                result[0] = resultL;
                result[1] = resultR;
                dValue.Add(result);
                counter++;
            }
            return dValue[dValue.Count - 1][1];
        }


        //Write in file keys
        private void writeKeys(int n, int e, int d, string privateK, string publicK) {
            using var filePu = new FileStream(publicK, FileMode.Create);
            using var writerPu = new BinaryWriter(filePu);
            byte[] PublicValue = Encoding.ASCII.GetBytes(n + "," + e); 
            writerPu.Write(PublicValue);
            writerPu.Close();

            using var filePri = new FileStream(privateK, FileMode.Create);
            using var writerPri = new BinaryWriter(filePri);
            byte[] PrivateValue = Encoding.ASCII.GetBytes(n + "," + d);

            writerPri.Write(PrivateValue);
            writerPri.Close();  
        }
        #endregion
    }
}
