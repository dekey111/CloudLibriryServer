using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.DataBase;
using Server.Models;
using System.Net.Sockets;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ClientTicketController : Controller
    {
        readonly CloudLibriryContext context;
        public ClientTicketController(CloudLibriryContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClientsTickets()
        {
            try
            {
                List<ClientTicketResponse> clientsTickets = new List<ClientTicketResponse>();
                context.ClientTickets.ToListAsync().Result.ForEach(x => clientsTickets.Add(new ClientTicketResponse(x)));
                return Ok(clientsTickets);
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpGet("GetClientTickets/{idClient}")]
        public async Task<IActionResult> GetClientTickets(int idClient)
        {
            try
            {
                List<ClientTicketResponse> clientTickets = new List<ClientTicketResponse>();
                context.ClientTickets.Where(x => x.IdClient == idClient).ToListAsync().Result.ForEach(x => clientTickets.Add(new ClientTicketResponse(x)));
                return Ok(clientTickets);
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpGet("GetOneClientTicket/{idTicket}")]
        public async Task<IActionResult> GetOneClientTicket(int idTicket)
        {
            try
            {
                var findClientTicket = await context.ClientTickets.FirstOrDefaultAsync(x => x.IdClientTicket == idTicket);
                if (findClientTicket == null)
                    return BadRequest("Формуляр не найден!");

                return Ok(new ClientTicketResponse(findClientTicket));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddingClientTicket([FromBody] ClientTicketRequest clientTicketReq)
        {
            try
            {
                var findClientTicket = await context.ClientTickets.FirstOrDefaultAsync(x => x.Number == clientTicketReq.Number);
                if (findClientTicket != null)
                    return BadRequest("Формулятор с таким номером уже существует!");

                ClientTicket newClientTicket = new ClientTicket()
                {
                    IdClient = clientTicketReq.IdClient,
                    Number = clientTicketReq.Number,
                    DateRegistration = clientTicketReq.DateRegistration,
                    Status = clientTicketReq.Status,
                    ValidityPeriod = clientTicketReq.ValidityPeriod
                };
                await context.ClientTickets.AddAsync(newClientTicket);
                await context.SaveChangesAsync();

                return Ok(new ClientTicketResponse(newClientTicket));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateClientTicket([FromBody] ClientTicketResponse clientTicketRes)
        {
            try
            {
                var findClientTicket = await context.ClientTickets.FirstOrDefaultAsync(x => x.IdClientTicket == clientTicketRes.IdClientTicket);
                if (findClientTicket == null)
                    return BadRequest("Формулятор не найден!");

                var findClientTicketNumber = await context.ClientTickets.FirstOrDefaultAsync(x => x.Number == clientTicketRes.Number);
                if (findClientTicketNumber != null)
                    return BadRequest("Формулятор с таким номером уже существует!");

                findClientTicket.IdClient = clientTicketRes.IdClient;
                findClientTicket.Number = clientTicketRes.Number;
                findClientTicket.DateRegistration = clientTicketRes.DateRegistration;
                findClientTicket.Status = clientTicketRes.Status;
                findClientTicket.ValidityPeriod = clientTicketRes.ValidityPeriod;

                context.ClientTickets.Update(findClientTicket);
                await context.SaveChangesAsync();

                return Ok(new ClientTicketResponse(findClientTicket));
            }
            catch (Exception ex)
            {
                return BadRequest($"Критическая ошибка! \nОписание ошибки: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClientTicket(int id)
        {
            try
            {
                var findClientTicket = await context.ClientTickets.FirstOrDefaultAsync(x => x.IdClientTicket == id);
                if (findClientTicket == null)
                    return BadRequest("Формуляр не найден!");

                context.ClientTickets.Remove(findClientTicket);
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
