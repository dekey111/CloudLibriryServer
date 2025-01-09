namespace Server.Models
{
    public class BooksRequest
    {
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
    }
}
