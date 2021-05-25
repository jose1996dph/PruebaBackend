using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace UsesCase.Helpers
{
    public static class Encrypted
    {
        public static string Sha256(string rawData)
        {
            using var sha256Hash = SHA256.Create();

            var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

            var builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
