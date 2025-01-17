namespace Server.Models
{
    public class TokenResponse
    {
        public int IdUser { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int IdRole { get; set; }

        public string Token { get; set; }
    }
}
