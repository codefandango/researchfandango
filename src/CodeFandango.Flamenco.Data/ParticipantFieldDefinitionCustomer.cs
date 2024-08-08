namespace CodeFandango.Flamenco.Data
{
    public class ParticipantFieldDefinitionCustomer
    {
        public required long ParticipantFieldDefinitionId { get; set; }
        public required long CustomerId { get; set; }
        public required ParticipantFieldDefinition ParticipantFieldDefinition { get; set; }
        public required Customer Customer { get; set; }
    }
}