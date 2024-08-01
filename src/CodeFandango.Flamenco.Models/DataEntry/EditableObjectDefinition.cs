using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Models.DataEntry
{
    public class EditableObjectDefinition
    {
        public required string Name { get; set; }
        public EditableObjectFlags Flags { get; set; }
        public EditableFieldCollection Fields { get; set; } = new EditableFieldCollection();
        public Dictionary<string, List<ReferenceObject>> References { get; set; } = new();
    }
}
