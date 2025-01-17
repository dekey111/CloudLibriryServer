using Server.DataBase;

namespace Server.Models
{
    public class UserResponse
    {
        public UserResponse() { }

        public int IdUser { get; set; }

        public string Login { get; set; } = null!;

        public string Password { get; set; } = null!;

        public int IdRole { get; set; }

        public UserResponse(User user)
        {
            IdUser = user.IdUser;
            Login = user.Login;
            Password = user.Password;
            IdRole = user.IdRole;
        }
    }
}
