using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

/* This is just some code a ripped off from the web to encrypt and decrypt the SAP password stored
 * in the Settings.xml file.
 */

namespace ShopFloorLib
{

    public class CryptoSystem
    {
        private static string passPhrase = "M!t3hJ0hns0n";
        private static string saltValue = "s@1tValue";
        private static string hashAlgorithm = "MD5";
        private static int passwordIterations = 2;
        private static string initVector = "@1B2c3D4d5F7q7H8";
        private static int keySize = 256;

        public static string Encrypt(string plainText)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] keyBytes = password.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
                cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] cipherTextBytes = memoryStream.ToArray();
                memoryStream.Close();
                cryptoStream.Close();
                string cipherText = Convert.ToBase64String(cipherTextBytes);
                return cipherText;
            }
            catch(Exception e)
            {
                MessageLogger.Add("Error Encrypting password " + e.ToString(), MessageLogger.MsgLevel.critical);
               return "";
            }
        }

        public static string Decrypt(string cipherText)
        {
            try
            {
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] saltValueBytes = Encoding.ASCII.GetBytes(saltValue);
                byte[] cipherTextBytes = Convert.FromBase64String(cipherText);
                PasswordDeriveBytes password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);
                byte[] keyBytes = password.GetBytes(keySize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                string plainText = Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
                return plainText;
            }
            catch (Exception e)
            {
                MessageLogger.Add("Error Decrypting password " + e.ToString(), MessageLogger.MsgLevel.critical);
                return "";
            }
        }
    }
}