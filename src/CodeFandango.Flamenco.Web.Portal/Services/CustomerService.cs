using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.Customers;
using CodeFandango.Flamenco.Models.DataEntry;
using CodeFandango.Flamenco.Models.Surveys;
using CodeFandango.Flamenco.Web.Portal.Interfaces;

namespace CodeFandango.Flamenco.Web.Portal.Services
{
    public class CustomerService : ObjectEditorService<Customer, CustomerModel>, ICustomerService
    {
        public CustomerService(IDataAccess da, IDataMapper mapper) : base(da, mapper)
        {
        }

        public override async Task<Success<EditableObjectDefinition>> GetDefinition()
        {
            var model = new EditableObjectDefinition
            {
                Name = "Customer",
                Fields = [
                    new EditableFieldModel
                    {
                        Name = "Name",
                        Code = "name",
                        IsRequired = true,
                        Order = 1,
                        ShowInList = true,
                        Description = "The name of the customer",
                        Type = EditableDataType.String,
                        Group = "General"
                    },
                    new EditableFieldModel
                    {
                        Name = "Description",
                        Code = "description",
                        IsRequired = false,
                        Order = 2,
                        ShowInList = true,
                        Description = "A description of the customer",
                        Type = EditableDataType.Text,
                        Group = "General"
                    },
                    new EditableFieldModel
                    {
                        Name = "Logo",
                        Code = "logo",
                        IsRequired = false,
                        Order = 3,
                        ShowInList = false,
                        Description = "The customer's logo",
                        Type = EditableDataType.Image,
                        Group = "Branding"
                    },
                ]
            };
            return new Success<EditableObjectDefinition>(model);
        }
    }
}
