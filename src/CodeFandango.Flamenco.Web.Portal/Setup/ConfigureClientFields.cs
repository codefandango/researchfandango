using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.Models.DataEntry;

namespace CodeFandango.Flamenco.Web.Portal.Setup
{
    public class ConfigureClientFields : IOneTimeSetup
    {
        private readonly FlamencoDb db;

        public ConfigureClientFields(FlamencoDb db)
        {
            this.db = db;
        }

        public void Run()
        {
            List<ClientField> fields = new List<ClientField>
            {
                new ClientField{ Group = "Details", Order = 1, Name = "Name", Code = "name", Description = "The name of the client", IsRequired = true, Type = EditableDataType.String },
                new ClientField{ Group = "Branding", Order = 1, Name = "Logo", Code = "logo", Description = "The client's logo", IsRequired = false, Type = EditableDataType.Image },
                new ClientField{ Group = "Branding", Order = 2, Name = "Primary Color", Code = "primarycolor", Description = "The client's primary brand color", IsRequired = false, Type = EditableDataType.Color },
                new ClientField{ Group = "Branding", Order = 3, Name = "Secondary Color", Code = "secondarycolor", Description = "The client's secondary brand color", IsRequired = false, Type = EditableDataType.Color }
            };

            foreach (ClientField field in fields)
            {
                if (!db.ClientFields.Any(f => f.Code == field.Code))
                {
                    db.ClientFields.Add(field);
                }
            }

            db.SaveChanges();
        }
    }
}
    