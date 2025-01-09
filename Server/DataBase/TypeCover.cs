using System;
using System.Collections.Generic;

namespace Server.DataBase;

public partial class TypeCover
{
    public int IdTypeCover { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
