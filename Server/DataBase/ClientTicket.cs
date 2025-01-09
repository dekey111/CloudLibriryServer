using System;
using System.Collections.Generic;

namespace Server.DataBase;

public partial class ClientTicket
{
    public int IdClientTicket { get; set; }

    public int IdClient { get; set; }

    public string Number { get; set; } = null!;

    public DateTime DateRegistration { get; set; }

    public bool Status { get; set; }

    public int ValidityPeriod { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Client IdClientNavigation { get; set; } = null!;
}
