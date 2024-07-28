using CodeFandango.Flamenco.Models.DataEntry;

namespace CodeFandango.Flamenco.Data
{
    public class EditableFieldDefinition
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public required string Code { get; set; }
        public EditableDataType Type { get; set; }
        public bool IsRequired { get; set; }
        public int Order { get; set; }
        public string? Group { get; set; }
        public bool ShowInList { get; set; }
    }
}