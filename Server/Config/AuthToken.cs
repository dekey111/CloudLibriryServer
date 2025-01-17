using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Server.Config
{
    public class AuthToken
    {
        public const string ISSUER = "Ilyadekey111";
        public const string AUDIENCE = "CloudLibriry";
        const string KEY = "IlyasVeryLongSecretKeyThatIsAtLeast32Chars";
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
