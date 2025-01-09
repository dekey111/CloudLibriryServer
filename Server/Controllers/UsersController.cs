using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Server.Config;
using Server.DataBase;
using Server.Models;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        readonly CloudLibriryContext context;
        public UsersController(CloudLibriryContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<UserResponse> users = new List<UserResponse>();
                context.Users.ToListAsync().Result.ForEach(x => users.Add(new UserResponse(x)));
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneUser(int id)
        {
            try
            {
                var findUser = await context.Users.FirstOrDefaultAsync(x => x.IdUser == id);
                if (findUser == null)
                    return BadRequest("Пользователь не найден!");

                return Ok(new UserResponse(findUser));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddingUser([FromBody] UserRequest userReq)
        {
            try
            {
                var findUser = await context.Users.FirstOrDefaultAsync(x => x.Login == userReq.Login);
                if (findUser != null)
                    return BadRequest("Такой пользователь уже существует!");

                User newUser = new User()
                {
                    Login = userReq.Login,
                    Password = md5.hashPassword(userReq.Password),
                    IdRole = userReq.IdRole
                };
                await context.Users.AddAsync(newUser);
                await context.SaveChangesAsync();

                return Ok(new UserResponse(newUser));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserResponse userRes)
        {
            try
            {
                var findUser = await context.Users.FirstOrDefaultAsync(x => x.IdUser == userRes.IdUser);
                if (findUser == null)
                    return BadRequest("Пользователь не найден!");


                var findUserLogin = await context.Users.FirstOrDefaultAsync(x => x.Login == userRes.Login);
                if (findUserLogin != null)
                    return BadRequest("Такой пользователь уже существует!");

                findUser.Login = userRes.Login;
                findUser.Password = md5.hashPassword(userRes.Password);
                findUser.IdRole = userRes.IdRole;

                context.Users.Update(findUser);
                await context.SaveChangesAsync();

                return Ok(new UserResponse(findUser));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var findUser = await context.Users.FirstOrDefaultAsync(x => x.IdUser == id);
                if (findUser == null)
                    return BadRequest("Пользователь не найден!");

                context.Users.Remove(findUser);
                await context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

    }
}
