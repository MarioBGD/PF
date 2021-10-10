using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PF.Common.Crypto
{
    public interface IHasher
    {
        string HashToBase64String(string value);
        byte[] HashToBytes(byte[] value);
    }
}
