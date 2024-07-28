using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.DataEntry;

namespace CodeFandango.Flamenco.DataAccess
{
    public class ClientDataAccess
    {
        private IDataAccess dataAccess;

        public ClientDataAccess(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public bool IsClientConfigured()
        {
            var requiredFieldIds = dataAccess.Database.ClientFields.Where(x => x.IsRequired).Select(x => x.Id).ToList();
            return requiredFieldIds.All(x => dataAccess.Database.Client.Any(y => y.ClientFieldId == x));
        }

        public void SetClientValue(string fieldCode, string value)
        {
            var field = dataAccess.Database.ClientFields.FirstOrDefault(x => x.Code == fieldCode);
            if (field == null)
            {
                throw new Exception($"Field with code {fieldCode} not found");
            }

            var client = dataAccess.Database.Client.FirstOrDefault(x => x.ClientFieldId == field.Id);
            if (client == null)
            {
                client = new ClientData
                {
                    ClientFieldId = field.Id,
                    Value = value
                };
                dataAccess.Database.Client.Add(client);
            }
            else
            {
                client.Value = value;
            }

            dataAccess.Database.SaveChanges();
        }

        public Success<EditableFormModel> GetEditableFields()
        {
            var list = dataAccess.Database.ClientFields.Select(x => new EditableFieldModel
            {
                Name = x.Name,
                Description = x.Description,
                Code = x.Code,
                Type = x.Type,
                IsRequired = x.IsRequired,
                Order = x.Order,
                Group = x.Group
            }).ToList();

            var editableFormModel = new EditableFormModel(list);

            var answers = dataAccess.Database.Client.Select(x => new FieldAnswerModel
            {
                FieldCode = x.ClientField.Code,
                Value = x.Value
            }).ToList();

            editableFormModel.Values.AddRange(answers);

            return new Success<EditableFormModel>(editableFormModel);
        }

        public Success SaveFieldValues(Dictionary<string, string> fields)
        {  
            foreach (var field in fields)
            {
                SetClientValue(field.Key, field.Value);
            }

            return new Success(true);
        }
    }
}