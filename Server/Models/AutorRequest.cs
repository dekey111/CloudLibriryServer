namespace Server.Models
{
    public class AutorRequest
    {
        public string Surname { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Patronymic { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string CountryOfBirth { get; set; } = null!;

        public string? Biography { get; set; }

        public string Fio { get; set; } = null!;
    }
}
