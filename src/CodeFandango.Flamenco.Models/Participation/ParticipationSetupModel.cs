using CodeFandango.Flamenco.Models.Customers;
using CodeFandango.Flamenco.Models.DataEntry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Models.Participation
{
    public class ParticipationSetupModel
    {
        public List<ParticipantFieldDefinitionModel> ParticipationFieldDefinitions { get; set; } = new();
        public List<CustomerModel> PermittedCustomers { get; set; } = new();
        public EditableFormModel ParticipantFieldEditDefinition { get; set; } = null!;
    }
}
