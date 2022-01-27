using System;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ADOPM3_10_05
{
    class Program
    {
        static void Main(string[] args)
        {
            //Hash a password using salt and streching
            byte[] encrypted = KeyDerivation.Pbkdf2(
                password: "Mupparnasjulsaga",
                salt: Encoding.UTF8.GetBytes("j78Y#p)/saREN!y3@"),
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 100,
                numBytesRequested: 64);

            foreach (byte b in encrypted) Console.Write($"{b:x2} ");

            string Base64encryption = Convert.ToBase64String(encrypted);
            Console.WriteLine(Base64encryption);

        }
    }
}
