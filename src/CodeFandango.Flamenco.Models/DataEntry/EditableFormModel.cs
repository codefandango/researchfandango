using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Models.DataEntry
{
    public class EditableFormModel
    {
        public EditableFormModel()
        {
        }

        public EditableFormModel(List<EditableFieldModel> fields)
        {
            this.Fields = fields;
        }

        public List<EditableFieldModel> Fields { get; set; } = new List<EditableFieldModel>();
        public List<FieldAnswerModel> Values { get; set; } = new List<FieldAnswerModel>();
        public string Name { get; set; }
    }
}
