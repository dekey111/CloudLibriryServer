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
    public class PublishersController : Controller
    {
        readonly CloudLibriryContext context;
        public PublishersController(CloudLibriryContext _context)
        {
            context = _context;
        }   

        [HttpGet]
        public async Task<IActionResult> GetAllPublishers()
        {
            try
            {
                List<PublisherResponse> publishers = new List<PublisherResponse>();
                context.Publishers.ToListAsync().Result.ForEach(x => publishers.Add(new PublisherResponse(x)));
                return Ok(publishers);
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOnePublisher(int id)
        {
            try
            {
                var findPublisher = await context.Publishers.FirstOrDefaultAsync(x => x.IdPublisher == id);
                if (findPublisher == null)
                    return BadRequest("Издатель не найден!");

                return Ok(new PublisherResponse(findPublisher));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddingPublisher([FromBody] PublisherRequest publisherReq)
        {
            try
            {
                var findPublisher = await context.Publishers.FirstOrDefaultAsync(x => x.Name == publisherReq.Name);
                if (findPublisher != null)
                    return BadRequest("Такой издатель уже существует!");

                Publisher newPublisher = new Publisher()
                {
                    Name = publisherReq.Name
                };
                await context.Publishers.AddAsync(newPublisher);
                await context.SaveChangesAsync();

                return Ok(new PublisherResponse(newPublisher));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdatePublisher([FromBody] PublisherResponse publisherRes)
        {
            try
            {
                var findPublisher = await context.Publishers.FirstOrDefaultAsync(x => x.IdPublisher == publisherRes.IdPublisher);
                if (findPublisher == null)
                    return BadRequest("Издатель не найден!");

                var findPublisherName = await context.Publishers.FirstOrDefaultAsync(x => x.Name.Trim() == publisherRes.Name.Trim());
                if (findPublisherName != null)
                    return BadRequest("Издатель с таким именем уже существует!");

                findPublisher.Name = publisherRes.Name;

                context.Publishers.Update(findPublisher);
                await context.SaveChangesAsync();
                return Ok(new PublisherResponse(findPublisher));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            try
            {
                var findPublisher = await context.Publishers.FirstOrDefaultAsync(x => x.IdPublisher == id);
                if (findPublisher == null)
                    return BadRequest("Издатель не найден!");

                context.Publishers.Remove(findPublisher);
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
