namespace Server.Models
{
    public class ClientTicketRequest
    {
        public int IdClient { get; set; }

        public string Number { get; set; } = null!;

        public DateTime DateRegistration { get; set; }

        public bool Status { get; set; }

        public int ValidityPeriod { get; set; }
    }
}
