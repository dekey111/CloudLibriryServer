namespace Server.Models
{
    public class UserRequest
    {
        public string Login { get; set; } = null!;
        public string Password { get; set; } = null!;
        public int IdRole { get; set; }
    }
}
