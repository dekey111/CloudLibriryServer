using Server.DataBase;

namespace Server.Models
{
    public class ClientResponse
    {
        public ClientResponse() { }

        public int IdClient { get; set; }

        public string Surname { get; set; }

        public string Name { get; set; }

        public string? Patronymic { get; set; }

        public string Adress { get; set; }

        public string RegistrationAdress { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Fio { get; set; }

        public ClientResponse(Client client)
        {
            IdClient = client.IdClient;
            Surname = client.Surname;
            Name = client.Name;
            Patronymic = client.Patronymic;
            Adress = client.Adress;
            RegistrationAdress = client.RegistrationAdress;
            Phone = client.Phone;
            Email = client.Email;
            Fio = client.Fio;
        }
    }
}
