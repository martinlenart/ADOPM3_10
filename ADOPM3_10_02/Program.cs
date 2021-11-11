using System;
using System.IO;
using System.Security.Cryptography;

namespace ADOPM3_10_02
{
    class Program
    {
        static void Main(string[] args)
        {
            //Explcicitly set the key and iv
            byte[] key = { 145, 12, 32, 245, 98, 132, 98, 214, 6, 77, 131, 44, 221, 3, 9, 50 };
            byte[] iv = { 15, 122, 132, 5, 93, 198, 44, 31, 9, 39, 241, 49, 250, 188, 80, 7 };

            //If you randomly generate key and iv, they need to be stored in a file or you cannot decrypt
            //RandomNumberGenerator rand = RandomNumberGenerator.Create();
            //rand.GetBytes(key);
            //rand.GetBytes(iv);

            byte[] dataset = { 1, 2, 3, 4, 5 };   // This is what we're encrypting.

            //Encrypt using AES
            foreach (byte b in dataset) Console.Write($"{b:x2} ");

            using (SymmetricAlgorithm algorithm = Aes.Create())
            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv))
            using (Stream f = File.Create(fname("Example11_02.bin")))
            using (Stream c = new CryptoStream(f, encryptor, CryptoStreamMode.Write))
                c.Write(dataset, 0, dataset.Length);

            //Decrypt using AES
            Console.WriteLine();
            byte[] decryptedDataset = new byte[5];

            using (SymmetricAlgorithm algorithm = Aes.Create())
            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
            using (Stream f = File.OpenRead(fname("Example11_02.bin")))
            using (Stream c = new CryptoStream(f, decryptor, CryptoStreamMode.Read))
                c.Read(decryptedDataset, 0, decryptedDataset.Length);

            foreach (byte b in decryptedDataset) Console.Write($"{b:x2} "); 

            static string fname(string name)
            {
                var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                documentPath = Path.Combine(documentPath, "AOOP2", "Examples");
                if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
                return Path.Combine(documentPath, name);
            }
        }
    }
    //Exercise:
    //1.    Encrypt and Decrypt using memorystream instead of FileStream
    //2.    Generate random key and iv and store it in a key-file in Base64 string format
    //3.    Decrypt and encrypted file by first reading in the key and iv from the key-file
}
