using System.Text;
using System.Security.Cryptography;

namespace LecturerService.Data
{
    public class Security
    {
        static HashAlgorithm algorithm = SHA256.Create();

        public static string GetHash(string pass)
        {
            StringBuilder str = new StringBuilder();
            foreach (byte b in algorithm.ComputeHash(Encoding.UTF8.GetBytes(pass)))
                str.Append(b.ToString("X2"));
            return str.ToString();
        }
    }
}