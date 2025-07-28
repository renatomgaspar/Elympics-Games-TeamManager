namespace Elympics_Games.API.Data.Entities
{
    public class Team : IEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Country { get; set; }
        public required int ElementsNumber { get; set; }
    }
}
