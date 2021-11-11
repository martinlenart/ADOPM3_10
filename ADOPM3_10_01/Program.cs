using System;
using System.IO;
using System.Security.Cryptography;

namespace ADOPM3_10_01
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] hash;

            // Compute hash from file:
            File.WriteAllText(fname("Example11_01.txt"), @"The quick brown fox jumps over the lazy dog.
	                                    The quick brown fox jumps over the lazy dog.
	                                    The quick brown fox jumps over the lazy dog.");

            using (Stream fs = File.OpenRead(fname("Example11_01.txt")))
                hash = SHA1.Create().ComputeHash(fs);   // SHA1 hash is 20 bytes long

            foreach (byte b in hash) Console.Write($"{b:x2} "); //"Hash from Example11_01.txt";
            Console.WriteLine();

            // Compute hash from string:
            byte[] data = System.Text.Encoding.UTF8.GetBytes("stRhong%pword");
            byte[] hash2 = SHA256.Create().ComputeHash(data);

            foreach (byte b in hash2) Console.Write($"{b:x2} "); // Hash from string;

            static string fname(string name)
            {
                var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                documentPath = Path.Combine(documentPath, "AOOP2", "Examples");
                if (!Directory.Exists(documentPath)) Directory.CreateDirectory(documentPath);
                return Path.Combine(documentPath, name);
            }
        }

        //Exercise:
        //1.    Modify file content and string content and generate new Hash. Compare with old
        //2.    Convert the Encrypted string to Base64 and printout. 
    }
}
