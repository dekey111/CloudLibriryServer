using System.Security.Cryptography;
using System.Text;


namespace Server.Config
{
    /// <summary>
    /// Класс для шифрования пароля в БД
    /// </summary>
    public class md5
    {
        /// <summary>
        /// Метод шифрования пароля в БД
        /// </summary>
        /// <param name="password">Принимает пароль</param>
        /// <returns>Возвращает хеш пароля</returns>
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
