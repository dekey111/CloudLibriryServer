using Server.DataBase;

namespace Server.Models
{
    public class ClientTicketResponse
    {
        public ClientTicketResponse() { }

        public int IdClientTicket { get; set; }

        public int IdClient { get; set; }

        public string Number { get; set; } = null!;

        public DateTime DateRegistration { get; set; }

        public bool Status { get; set; }

        public int ValidityPeriod { get; set; }

        public ClientTicketResponse(ClientTicket clientTicket)
        {
            IdClientTicket = clientTicket.IdClientTicket;
            IdClient = clientTicket.IdClient;
            Number = clientTicket.Number;
            DateRegistration = clientTicket.DateRegistration;
            Status = clientTicket.Status;
            ValidityPeriod = clientTicket.ValidityPeriod;
        }
    }
}
