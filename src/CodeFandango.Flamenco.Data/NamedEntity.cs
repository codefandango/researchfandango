namespace CodeFandango.Flamenco.Data
{
    public class NamedEntity
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }
}