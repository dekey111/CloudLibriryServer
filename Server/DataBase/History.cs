using System;
using System.Collections.Generic;

namespace Server.DataBase;

public partial class History
{
    public int IdHistory { get; set; }

    public int IdClientTickert { get; set; }

    public int IdBook { get; set; }

    public bool Action { get; set; }

    public DateTime Date { get; set; }

    public int ValidityPeriod { get; set; }

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual ClientTicket IdClientTickertNavigation { get; set; } = null!;
}
