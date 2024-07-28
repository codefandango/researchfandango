using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Models.DataEntry
{
    public class EditableFormModel
    {
        public EditableFormModel(List<EditableFieldModel> fields)
        {
            this.Fields = fields;
        }

        public List<EditableFieldModel> Fields { get; } = new List<EditableFieldModel>();
        public List<FieldAnswerModel> Values { get; } = new List<FieldAnswerModel>();
    }
}
