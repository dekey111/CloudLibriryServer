using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Config;
using Server.DataBase;
using Server.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Server.Controllers
{
    /// <summary>
    /// Контроллер для авторизации
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        readonly CloudLibriryContext _context;
        public AuthController(CloudLibriryContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Метод получения JWT-токена
        /// </summary>
        /// <param name="token">Принимает параметры токена</param>
        /// <returns>Возвращает </returns>
        [HttpGet("GetToken")]
        public IActionResult GetToken([FromBody] TokenRequest token)
        {
            var passwordHash = md5.hashPassword(token.Password);

            var user = _context.Users.FirstOrDefault(x => x.Login == token.Login);
            if (user == null)
                return BadRequest("Пользователь не найден!");

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Login) };
            var jwt = new JwtSecurityToken( issuer: AuthToken.ISSUER, audience: AuthToken.AUDIENCE,
            claims: claims,
                    expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                    signingCredentials: new SigningCredentials(AuthToken.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature));
            
            TokenResponse userResponse = new TokenResponse()
            {
                IdUser = user.IdUser,
                Login = user.Login,
                Password = passwordHash,
                IdRole = user.IdRole,
                Token = new JwtSecurityTokenHandler().WriteToken(jwt)
            };
            return Ok();
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Authentication()
        {
            return Ok();
        }
    }
}
