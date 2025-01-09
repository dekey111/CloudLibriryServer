using System;
using System.Collections.Generic;

namespace Server.DataBase;

public partial class Autor
{
    public int IdAutor { get; set; }

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Patronymic { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }

    public string CountryOfBirth { get; set; } = null!;

    public string? Biography { get; set; }

    public string Fio { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
