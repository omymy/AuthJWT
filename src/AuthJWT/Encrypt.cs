using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AuthJWT
{
    public static class Encrypt
    {
        public static string MDString(this string message)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.Default.GetBytes(message);
            byte[] bytes = md5.ComputeHash(buffer);
            return bytes.Aggregate("", (current, b) => current + b.ToString("x2"));
        }
    }
}
