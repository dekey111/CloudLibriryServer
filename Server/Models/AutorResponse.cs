using Server.DataBase;

namespace Server.Models
{
    public class AutorResponse
    {
        public AutorResponse() { }

        public int IdAutor { get; set; }

        public string Surname { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string Patronymic { get; set; } = null!;

        public DateTime DateOfBirth { get; set; }

        public string CountryOfBirth { get; set; } = null!;

        public string? Biography { get; set; }

        public string Fio { get; set; } = null!;

        public AutorResponse(Autor author)
        {
            IdAutor = author.IdAutor;
            Surname = author.Surname;
            Name = author.Name;
            Patronymic = author.Patronymic;
            DateOfBirth = author.DateOfBirth;
            CountryOfBirth = author.CountryOfBirth;
            Biography = author.Biography;
            Fio = author.Fio;
        }
    }
}
