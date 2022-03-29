using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TriviaProject
{
    public class SecurityHelper
    {

        public static string GetFilesCode()
        {
            return DecryptUnlockerFile();
        }

        ///<summary>
        /// Decrypts a file using Rijndael algorithm.
        ///</summary>
        ///<param name="inputFile"></param>
        ///<param name="outputFile"></param>
        static string DecryptUnlockerFile()
        {

            try
            {
                string fileName = "files/unlocker.txt";
                string inputFile = Path.Combine(Directory.GetCurrentDirectory(), fileName);

                string encriptionKey = @"Inter360";
                UnicodeEncoding UE = new UnicodeEncoding();
                byte[] key = UE.GetBytes(encriptionKey);

                FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);

                RijndaelManaged RMCrypto = new RijndaelManaged();

                CryptoStream cs = new CryptoStream(fsCrypt,
                    RMCrypto.CreateDecryptor(key, key),
                    CryptoStreamMode.Read);

                string res;
                byte[] der = new byte[0];
                int data;
                while ((data = cs.ReadByte()) != -1)
                    der = addByteToArray(der, (byte)data);

                res = Encoding.UTF8.GetString(der);
                cs.Close();
                fsCrypt.Close();

                return res;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static byte[] addByteToArray(byte[] bArray, byte newByte)
        {
            byte[] newArray = new byte[bArray.Length + 1];
            bArray.CopyTo(newArray, 0);
            newArray[bArray.Length] = newByte;
            return newArray;
        }


        ///<summary>
        /// Encrypts a file using Rijndael algorithm.
        ///</summary>
        //private void EncryptUnlockerFile()
        //{

        //    try
        //    {
        //        string fileName = "files/pass.txt";
        //        string destinationFilename = "files/unlocker.txt";

        //        string inputFile = Path.Combine(Directory.GetCurrentDirectory(), fileName);
        //        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), destinationFilename);
        //        string password = @"Inter360"; // Your Key Here
        //        UnicodeEncoding UE = new UnicodeEncoding();
        //        byte[] key = UE.GetBytes(password);

        //        string cryptFile = outputFile;
        //        FileStream fsCrypt = new FileStream(cryptFile, FileMode.Create);

        //        RijndaelManaged RMCrypto = new RijndaelManaged();

        //        CryptoStream cs = new CryptoStream(fsCrypt,
        //            RMCrypto.CreateEncryptor(key, key),
        //            CryptoStreamMode.Write);

        //        FileStream fsIn = new FileStream(inputFile, FileMode.Open);

        //        int data;
        //        while ((data = fsIn.ReadByte()) != -1)
        //            cs.WriteByte((byte)data);


        //        fsIn.Close();
        //        cs.Close();
        //        fsCrypt.Close();
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //}
    }
}
