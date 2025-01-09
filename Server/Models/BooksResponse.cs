using Server.DataBase;

namespace Server.Models
{
    public class BooksResponse
    {
        public BooksResponse() { }

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

        public BooksResponse(Book book)
        {
            IdBooks = book.IdBooks;
            Isbn = book.Isbn;
            Name = book.Name;
            IdAutor = book.IdAutor;
            IdPublisher = book.IdPublisher;
            DatePublished = book.DatePublished;
            IdTypeCover = book.IdTypeCover;
            Language = book.Language;
            CountPages = book.CountPages;
            Desctiprtion = book.Desctiprtion;
            Cost = book.Cost;
        }
    }
}
