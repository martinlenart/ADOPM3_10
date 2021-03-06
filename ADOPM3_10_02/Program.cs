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
            byte[] key = new byte[16]; // { 145, 12, 32, 245, 98, 132, 98, 214, 6, 77, 131, 44, 221, 3, 9, 50 };
            byte[] iv = new byte[16];  // { 15, 122, 132, 5, 93, 198, 44, 31, 9, 39, 241, 49, 250, 188, 80, 7 };

            //If you randomly generate key and iv, they need to be stored in a file or you cannot decrypt
            //Key is stored in a secrect Key-repository, while the iv can be shared openly before encrypt/decrypt
            RandomNumberGenerator rand = RandomNumberGenerator.Create();
            rand.GetBytes(key);
            rand.GetBytes(iv);

            var stringToEncrypt = "The quick brown fox jumps over the lazy dog.";

            // This is what we're encrypting.
            Console.WriteLine("This is what we're encrypting and write encrypted to file.");
            Console.WriteLine($"As string: {stringToEncrypt}");
 
            Console.WriteLine($"As byte[]:");
            byte[] dataset = System.Text.Encoding.Unicode.GetBytes(stringToEncrypt);
            foreach (byte b in dataset) Console.Write($"{b:x2} ");

            //Encrypt using AES
            using (SymmetricAlgorithm algorithm = Aes.Create())
            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, iv))
            using (Stream f = File.Create(fname("Example11_02.bin")))
            using (Stream c = new CryptoStream(f, encryptor, CryptoStreamMode.Write))
                c.Write(dataset, 0, dataset.Length);

            //Decrypt using AES
            byte[] decryptedDataset = new byte[dataset.Length];

            using (SymmetricAlgorithm algorithm = Aes.Create())
            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, iv))
            using (Stream f = File.OpenRead(fname("Example11_02.bin")))
            using (Stream c = new CryptoStream(f, decryptor, CryptoStreamMode.Read))
                c.Read(decryptedDataset, 0, decryptedDataset.Length);

            Console.WriteLine("\n\nThis is what we decrypted from the file.");

            string decryptedString = System.Text.Encoding.Unicode.GetString(decryptedDataset);
            Console.WriteLine($"As string: {decryptedString}");

            Console.WriteLine($"As byte[]:");
            foreach (byte b in decryptedDataset) Console.Write($"{b:x2} ");
            Console.WriteLine();

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
    //1.    Generate random key and iv and store it in a key-file in Base64 string format
    //2.    Decrypt and encrypted file by first reading in the key and iv from the key-file
}
