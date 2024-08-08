using CodeFandango.Flamenco.Models.Participation;

namespace CodeFandango.Flamenco.Data
{
    public class ParticipantFieldDefinition : UniquelyCodedEntity
    {
        public ParticipantFieldType Type { get; set; }      
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        public string? Group { get; set; }
        public bool ShowInList { get; set; }
        public ICollection<ParticipantFieldDefinitionCustomer> Customers { get; set; } = new HashSet<ParticipantFieldDefinitionCustomer>();
        public long? SurveyId { get; set; }
        public Survey? Survey { get; set; }
        public long? StudyId { get; set; }
        public Study? Study { get; set; }
    }
}