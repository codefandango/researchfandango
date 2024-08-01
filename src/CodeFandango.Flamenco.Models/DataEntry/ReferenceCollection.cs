namespace CodeFandango.Flamenco.Models.DataEntry
{

    public class ReferenceCollection : List<ReferenceObject>
    {

    }
    public class ReferenceObject
    {
        public long Id { get; set; }
        public required string Name { get; set; } 
    }
}