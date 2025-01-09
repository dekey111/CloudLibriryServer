using System.Security.Cryptography;
using System.Text;

namespace Server.Config
{
    public class md5
    {
        public static string hashPassword(string password)
        {
            MD5 md5 = MD5.Create();

            byte[] b = Encoding.ASCII.GetBytes(password);
            byte[] hash = md5.ComputeHash(b);

            StringBuilder sb = new StringBuilder();
            foreach (var item in hash)
                sb.Append(item.ToString("X2"));

            return sb.ToString();
        }
    }
}
