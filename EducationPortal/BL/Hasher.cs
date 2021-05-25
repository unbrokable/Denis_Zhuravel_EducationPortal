using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
namespace Application
{
    public class Hasher:IHasher
    {
       public string Hash(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s); 
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = string.Empty;
            foreach (byte b in byteHash)
                hash += string.Format("{0:x2}", b);
            return hash;
        }
    }
}
