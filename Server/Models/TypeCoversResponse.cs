using Server.DataBase;

namespace Server.Models
{
    public class TypeCoversResponse
    {
        public TypeCoversResponse() { }

        public int IdTypeCover { get; set; }

        public string Name { get; set; } = null!;

        public TypeCoversResponse(TypeCover typeCover)
        {
            IdTypeCover = typeCover.IdTypeCover;
            Name = typeCover.Name;
        }
    }
}
