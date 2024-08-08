using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Models.DataEntry
{
    public class EditableFieldModel
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Code { get; set; }
        public EditableDataType Type { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        public string? Group { get; set; }
        public bool ShowInList { get; set; }
        public string? ObjectReference { get; set; }
        public string FieldReference { get; set; }
        public string UniqueScope { get; set; }
    }
}
