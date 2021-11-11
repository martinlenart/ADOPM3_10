using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;

namespace ADOPM3_10_03
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create public/private keypair, and save to disk:
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                File.WriteAllText(fname("Example11_03_PublicKeyOnly.xml"), rsa.ToXmlString(false));
                File.WriteAllText(fname("Example11_03_PublicPrivate.xml"), rsa.ToXmlString(true));
            }

            // Encrypt. Small message, typically a key for symmetric encryption
            byte[] data = Encoding.UTF8.GetBytes("Message to encrypt");

            string publicKeyOnly = File.ReadAllText(fname("Example11_03_PublicKeyOnly.xml"));
            string publicPrivate = File.ReadAllText(fname("Example11_03_PublicPrivate.xml"));

            byte[] encrypted, decrypted;
            using (var rsaPublicOnly = new RSACryptoServiceProvider())
            {
                rsaPublicOnly.FromXmlString(publicKeyOnly);
                encrypted = rsaPublicOnly.Encrypt(data, true);
           }

            // Decrypt
            using (var rsaPublicPrivate = new RSACryptoServiceProvider())
            {
                // With the private key we can successfully decrypt:
                rsaPublicPrivate.FromXmlString(publicPrivate);
                decrypted = rsaPublicPrivate.Decrypt(encrypted, true);
            }

            Console.WriteLine(Encoding.UTF8.GetString(decrypted)); // Message to encrypt

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
    //1.    Try to decrypt the message using the public Key. What happens?
    //2.    Tamper with the Key values in the XML file and try to Encrypt and Decrypt. What happens?
}
