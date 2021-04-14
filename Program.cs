using System;
using System.IO;
using System.Security.Cryptography;

namespace P1_2
{
    class Program
    {
        private static string Encrypt(byte[] key, string secretString)
        {
            DESCryptoServiceProvider csp = new DESCryptoServiceProvider();
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, csp.CreateEncryptor(key, key), CryptoStreamMode.Write);
            StreamWriter sw = new StreamWriter(cs);
            sw.Write(secretString);
            sw.Flush();
            cs.FlushFinalBlock();
            sw.Flush();
            return Convert.ToBase64String(ms.GetBuffer(), 0, (int)ms.Length);
        }
        static void Main(string[] args)
        {
        string[] cla = new string[Environment.GetCommandLineArgs().Length];
        cla = Environment.GetCommandLineArgs();
        string secretString = cla[1];
        string matchThis = cla[2];
        
        for(int i = 0; i <= 1440; i++) 
        {   
            DateTime dt = new DateTime(2020, 7, 3, 11, 0, 0);
            DateTime dtPlusOne = dt.AddMinutes(i);
            TimeSpan ts = dtPlusOne.Subtract(new DateTime(1970, 1, 1));
            Random rng = new Random((int)ts.TotalMinutes);
            byte[] key = BitConverter.GetBytes(rng.NextDouble());
            string encrypted = Encrypt(key, secretString);
            
            if (encrypted == matchThis)
            {
                Console.WriteLine(Convert.ToString((int)ts.TotalMinutes));
                break;
            }

        }
        

        
        

        //Console.WriteLine(Encrypt(key, secretString));


        }
    }
}
