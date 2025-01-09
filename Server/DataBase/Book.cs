using System;
using System.Collections.Generic;

namespace Server.DataBase;

public partial class Book
{
    public int IdBooks { get; set; }

    public string Isbn { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int IdAutor { get; set; }

    public int IdPublisher { get; set; }

    public string DatePublished { get; set; } = null!;

    public int IdTypeCover { get; set; }

    public string Language { get; set; } = null!;

    public string CountPages { get; set; } = null!;

    public string? Desctiprtion { get; set; }

    public decimal Cost { get; set; }

    public virtual ICollection<History> Histories { get; set; } = new List<History>();

    public virtual Autor IdAutorNavigation { get; set; } = null!;

    public virtual Publisher IdPublisherNavigation { get; set; } = null!;

    public virtual TypeCover IdTypeCoverNavigation { get; set; } = null!;
}
