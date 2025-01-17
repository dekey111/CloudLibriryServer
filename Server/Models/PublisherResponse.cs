using Server.DataBase;

namespace Server.Models
{
    public class PublisherResponse
    {
        public PublisherResponse() { }

        public int IdPublisher { get; set; }

        public string Name { get; set; } = null!;

        public PublisherResponse(Publisher publisher)
        {
            IdPublisher = publisher.IdPublisher;
            Name = publisher.Name;
        }
    }
}
