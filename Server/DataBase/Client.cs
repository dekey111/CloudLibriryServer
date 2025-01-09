using System;
using System.Collections.Generic;

namespace Server.DataBase;

public partial class Client
{
    public int IdClient { get; set; }

    public string? Surname { get; set; }

    public string? Name { get; set; }

    public string? Patronymic { get; set; }

    public string? Adress { get; set; }

    public string? RegistrationAdress { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Fio { get; set; }

    public virtual ICollection<ClientTicket> ClientTickets { get; set; } = new List<ClientTicket>();
}
