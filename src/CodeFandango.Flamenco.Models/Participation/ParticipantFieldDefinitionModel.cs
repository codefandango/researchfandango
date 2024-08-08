using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Models.Participation
{
    public class ParticipantFieldDefinitionModel : UniquelyCodedModel
    {
        public ParticipantFieldType Type { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        public string? Group { get; set; }
        public bool ShowInList { get; set; }
        public long? StudyId { get; set; }
        public long? SurveyId { get; set; }
    }
}
