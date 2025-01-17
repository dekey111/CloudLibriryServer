using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DataBase;
using Server.Models;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : Controller
    {
        readonly CloudLibriryContext context;

        public AuthorController(CloudLibriryContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAuthor()
        {
            try
            {
                List<AutorResponse> autors = new List<AutorResponse>();
                context.Autors.ToListAsync().Result.ForEach(x => autors.Add(new AutorResponse(x)));
                return Ok(autors);
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneAutor(int id)
        {
            try
            {
                var findAutor = await context.Autors.FirstOrDefaultAsync(x => x.IdAutor == id);
                if (findAutor == null)
                    return BadRequest("Автор не найден!");

                return Ok(new AutorResponse(findAutor));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddingAutor([FromBody] AutorRequest autorReq)
        {
            try
            {
                var findAutor = await context.Autors.FirstOrDefaultAsync(x => 
                x.Surname == autorReq.Surname
                && x.Name == autorReq.Name
                && x.Patronymic == autorReq.Patronymic
                && x.DateOfBirth == autorReq.DateOfBirth
                && x.CountryOfBirth == autorReq.CountryOfBirth);

                if (findAutor != null)
                    return BadRequest("Такой автор уже есть!");

                Autor newAutor = new Autor()
                {
                    Surname = autorReq.Surname,
                    Name = autorReq.Name,
                    Patronymic = autorReq.Patronymic,
                    DateOfBirth = autorReq.DateOfBirth,
                    CountryOfBirth = autorReq.CountryOfBirth,
                    Biography = autorReq.Biography
                };
                await context.Autors.AddAsync(newAutor);
                await context.SaveChangesAsync();

                return Ok(new AutorResponse(newAutor));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateAutor([FromBody] AutorResponse autorRes)
        {
            try
            {
                var findAutor = await context.Autors.FirstOrDefaultAsync(x => x.IdAutor == autorRes.IdAutor);
                if (findAutor == null)
                    return BadRequest("Автор не найден!");

                var findAutorName = await context.Autors.FirstOrDefaultAsync(x =>
                x.Surname == autorRes.Surname
                && x.Name == autorRes.Name
                && x.Patronymic == autorRes.Patronymic
                && x.DateOfBirth == autorRes.DateOfBirth
                && x.CountryOfBirth == autorRes.CountryOfBirth);

                if (findAutorName != null)
                    return BadRequest("Такой автор уже есть!");

                findAutor.Surname = autorRes.Surname;
                findAutor.Name = autorRes.Name;
                findAutor.Patronymic = autorRes.Patronymic;
                findAutor.DateOfBirth = autorRes.DateOfBirth;
                findAutor.CountryOfBirth = autorRes.CountryOfBirth;
                findAutor.Biography = autorRes.Biography;

                context.Autors.Update(findAutor);
                await context.SaveChangesAsync();

                return Ok(new AutorResponse(findAutor));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor(int id)
        {
            try
            {
                var findAutor = await context.Autors.FirstOrDefaultAsync(x => x.IdAutor == id);
                if (findAutor == null)
                    return BadRequest("Автор не найден!");

                context.Remove(findAutor);
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
