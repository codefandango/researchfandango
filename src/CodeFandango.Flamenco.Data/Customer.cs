namespace CodeFandango.Flamenco.Data
{
    public class Customer : NamedEntity
    {
        public long? LogoId { get; set; }
        public Media? Logo { get; set; }
        public ICollection<FlamencoIdentity> Users { get; set; } = new HashSet<FlamencoIdentity>();
    }
}