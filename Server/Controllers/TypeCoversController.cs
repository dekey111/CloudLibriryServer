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
    public class TypeCoversController : Controller
    {
        readonly CloudLibriryContext context;
        public TypeCoversController(CloudLibriryContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTypeCovers()
        {
            try
            {
                List<TypeCoversResponse> typeCovers = new List<TypeCoversResponse>();
                context.TypeCovers.ToListAsync().Result.ForEach(x => typeCovers.Add(new TypeCoversResponse(x)));
                return Ok(typeCovers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneTypeCovers(int id)
        {
            try
            {
                var findTypeCover = await context.TypeCovers.FirstOrDefaultAsync(x => x.IdTypeCover == id);
                if (findTypeCover == null)
                    return BadRequest("Тип обложки не найден!");

                return Ok(new TypeCoversResponse(findTypeCover));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddingTypeCovers([FromBody] TypeCoversRequest typeCoversReq)
        {
            try
            {
                var findTypeCover = await context.TypeCovers.FirstOrDefaultAsync(x => x.Name == typeCoversReq.Name);
                if (findTypeCover != null)
                    return BadRequest("Такой тип обложки уже существует!");

                TypeCover newTypeCover = new TypeCover()
                {
                    Name = typeCoversReq.Name
                };

                await context.TypeCovers.AddAsync(newTypeCover);
                await context.SaveChangesAsync();

                return Ok(new TypeCoversResponse(newTypeCover));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTypeCovers([FromBody] TypeCoversResponse typeCoversRes)
        {
            try
            {
                var findTypeCover = await context.TypeCovers.FirstOrDefaultAsync(x => x.IdTypeCover == typeCoversRes.IdTypeCover);
                if (findTypeCover == null)
                    return BadRequest("Тип обложки не найден!");

                var findTypeCoverName = await context.TypeCovers.FirstOrDefaultAsync(x => x.Name == typeCoversRes.Name);
                if (findTypeCover != null)
                    return BadRequest("Такой тип обложки уже существует!");

                findTypeCover.Name = typeCoversRes.Name;

                context.TypeCovers.Update(findTypeCover);
                await context.SaveChangesAsync();

                return Ok(new TypeCoversResponse(findTypeCover));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTypeCovers(int id)
        {
            try
            {
                var findTypeCover = await context.TypeCovers.FirstOrDefaultAsync(x => x.IdTypeCover == id);
                if (findTypeCover == null)
                    return BadRequest("Тип обложки не найден!");

                context.TypeCovers.Remove(findTypeCover);
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
