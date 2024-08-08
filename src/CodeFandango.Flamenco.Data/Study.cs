namespace CodeFandango.Flamenco.Data
{
    public class Study : NamedEntity
    {
        public bool IsEnabled { get; set; }
        public ICollection<Survey> Surveys { get; set; } = new HashSet<Survey>();   
    }
}