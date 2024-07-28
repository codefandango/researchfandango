namespace CodeFandango.Flamenco.Data
{
    public class ClientData
    {
        public long Id { get; set; }
        public long ClientFieldId { get; set; }
        public ClientField? ClientField { get; set; }
        public required string Value { get; set; }
    }
}