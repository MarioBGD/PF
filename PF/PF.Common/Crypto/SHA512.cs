using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.Common.Crypto
{
    public class SHA512 : IHasher
    {
        private System.Security.Cryptography.SHA512 Hash;

        public SHA512()
        {
            Hash = new System.Security.Cryptography.SHA512Managed();
        }

        public string HashToBase64String(string value)
        {
            byte[] bytesValue = Encoding.Unicode.GetBytes(value);
            byte[] hashed = HashToBytes(bytesValue);
            string base64 = Convert.ToBase64String(hashed);
            return base64;
        }

        public byte[] HashToBytes(byte[] value)
        {
            byte[] hashed = Hash.ComputeHash(value);
            return hashed;
        }
    }
}
