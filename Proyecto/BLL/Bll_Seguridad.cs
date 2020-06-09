using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public static class Bll_Seguridad
    {

        // Encripta en Has256
        public static string ComputeHash(this string input, HashType hashType)
        { 
            byte[] numArray;
            byte[] bytes = Encoding.ASCII.GetBytes(input);

            using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(hashType.ToString().ToUpperInvariant()))
            {
                numArray = hashAlgorithm.ComputeHash(bytes);
            }
            return BitConverter.ToString(numArray).Replace("-", string.Empty).ToLower();
        }

    }
}
