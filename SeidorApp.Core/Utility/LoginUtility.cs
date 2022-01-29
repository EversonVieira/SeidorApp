using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SeidorApp.Core.Utility
{
    internal static class LoginUtility
    {
        private static readonly HMACSHA512 _hMACSHA512 = new HMACSHA512(UTF8Encoding.UTF8.GetBytes("SeidorSeed"));
        public static string EncryptPassword(string password)
        {
            return EncryptProp(password);
        }

        private static string EncryptProp(string value)
        {
            StringBuilder sb = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(value))
            {
                byte[] pkEncoded = UTF8Encoding.UTF8.GetBytes(value);

                byte[] hashedPkEncoded = _hMACSHA512.ComputeHash(pkEncoded);

                foreach (byte b in hashedPkEncoded)
                {
                    sb.Append(b.ToString("X2"));
                }
            }

            return sb.ToString();
        }
    }
}
