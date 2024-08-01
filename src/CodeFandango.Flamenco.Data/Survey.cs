namespace CodeFandango.Flamenco.Data
{
    public class Survey : NamedEntity
    {
        public long StudyId { get; set; }
        public Study Study { get; set; } = null!;
    }
}