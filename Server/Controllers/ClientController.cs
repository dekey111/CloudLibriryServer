using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DataBase;
using Server.Models;
using System.Numerics;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : Controller
    {
        readonly CloudLibriryContext context;
        public ClientController(CloudLibriryContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                List<ClientResponse> clients = new List<ClientResponse>();
                context.Clients.ToListAsync().Result.ForEach(x => clients.Add(new ClientResponse(x)));
                return Ok(clients);
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOneClients(int id)
        {
            try
            {
                var findClient = await context.Clients.FirstOrDefaultAsync(x => x.IdClient == id);
                if (findClient == null)
                    return BadRequest("Клиент не найден!");

                return Ok(new ClientResponse(findClient));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddingClient([FromBody] ClientRequest clientReq)
        {
            try
            {
                var findClient = await context.Clients.FirstOrDefaultAsync(x =>
                x.Surname == clientReq.Surname
                && x.Name == clientReq.Name
                && x.Adress == clientReq.Adress
                && x.RegistrationAdress == clientReq.RegistrationAdress
                && x.Phone == clientReq.Phone
                && x.Email == clientReq.Email);

                if (findClient != null)
                    return BadRequest("Такой клиент уже есть!");

                Client newClient = new Client()
                {
                    Surname = clientReq.Surname,
                    Name = clientReq.Name,
                    Patronymic = clientReq.Patronymic,
                    Adress = clientReq.Adress,
                    RegistrationAdress = clientReq.RegistrationAdress,
                    Phone = clientReq.Phone,
                    Email = clientReq.Email,
                };

                await context.Clients.AddAsync(newClient);
                await context.SaveChangesAsync();

                return Ok(new ClientResponse(newClient));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClient([FromBody] ClientResponse clientRes)
        {
            try
            {
                var findClient = await context.Clients.FirstOrDefaultAsync(x => x.IdClient == clientRes.IdClient);
                if (findClient == null)
                    return BadRequest("Клиент не найден!");

                var findClientFields = await context.Clients.FirstOrDefaultAsync(x =>
                x.Surname == clientRes.Surname
                && x.Name == clientRes.Name
                && x.Adress == clientRes.Adress
                && x.RegistrationAdress == clientRes.RegistrationAdress
                && x.Phone == clientRes.Phone
                && x.Email == clientRes.Email);

                if (findClientFields != null)
                    return BadRequest("Такой клиент уже есть!");


                findClient.Surname = clientRes.Surname;
                findClient.Name = clientRes.Name;
                findClient.Patronymic = clientRes.Patronymic;
                findClient.Adress = clientRes.Adress;
                findClient.RegistrationAdress = clientRes.RegistrationAdress;
                findClient.Phone = clientRes.Phone;
                findClient.Email = clientRes.Email;

                context.Clients.Update(findClient);
                await context.SaveChangesAsync();

                return Ok(new ClientResponse(findClient));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var findClient = await context.Clients.FirstOrDefaultAsync(x => x.IdClient == id);
                if (findClient == null)
                    return BadRequest("Клиент не найден!");

                context.Clients.Remove(findClient);
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
