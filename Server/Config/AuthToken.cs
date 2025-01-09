using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Server.Config
{
    public class AuthToken
    {
        public const string ISSUER = "Ilyadekey111";    // издатель токена
        public const string AUDIENCE = "CloudLibriry";  // потребитель токена
        const string KEY = "testKey123321";             // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
