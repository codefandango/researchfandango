namespace CodeFandango.Flamenco.Models.DataEntry
{
    public class ObjectActionDefinition
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public int Order { get; set; }
        public required string Icon { get; set; }
        public required string Action { get; set; }
        public string? Path { get; set; }
        public ObjectActionScope Scope { get; set; }
    }
}