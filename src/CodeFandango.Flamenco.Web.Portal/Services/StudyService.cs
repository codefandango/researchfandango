using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.DataEntry;
using CodeFandango.Flamenco.Models.Studies;
using CodeFandango.Flamenco.Web.Portal.Interfaces;

namespace CodeFandango.Flamenco.Web.Portal.Services
{
    internal class StudyService : ObjectEditorService<Study, StudyModel>, IStudyService
    {

        public StudyService(IDataAccess da, IDataMapper mapper) : base(da, mapper)
        {
        }

        public async Task<Success<StudyModel>> CreateOrUpdateStudy(StudyModel study)
        {

            if (study.Id == 0)
            {
                return await CreateStudy(study);
            }
            else
            {
                return await UpdateStudy(study);
            }

        }

        private async Task<Success<StudyModel>> UpdateStudy(StudyModel study)
        {
            try
            {
                var existingStudy = await data.Studies.GetObject(study.Id);
                if (existingStudy == null)
                {
                    return new Success<StudyModel>(false, "Study not found");
                }

                var updatedStudy = mapper.Map<StudyModel, Study>(study);
                var result = await data.Studies.UpdateObject(updatedStudy);
                if (result == null)
                {
                    return new Success<StudyModel>(false, "Failed to update study");
                }

                var model = mapper.Map<Study, StudyModel>(result);
                return new Success<StudyModel>(model);
            }
            catch (Exception ex)
            {
                return new Success<StudyModel>(false, ex.Message);
            }
        }

        private async Task<Success<StudyModel>> CreateStudy(StudyModel study)
        {
            try
            {
                var newStudy = mapper.Map<StudyModel, Study>(study);
                var result = await data.Studies.CreateObject(newStudy);
                if (result == null)
                {
                    return new Success<StudyModel>(false, "Failed to create study");
                }

                var model = mapper.Map<Study, StudyModel>(result);
                return new Success<StudyModel>(model);
            }
            catch (Exception ex)
            {
                return new Success<StudyModel>(false, ex.Message);
            }
        }

        public override async Task<Success<EditableObjectDefinition>> GetDefinition()
        {
            var model = new EditableObjectDefinition()
            {
                Name = "Study",
            };

            var fields = new EditableFieldCollection()
            {
                new EditableFieldModel()
                {
                    Name = "Name",
                    Description = "The name of the study",
                    Code = "name",
                    Type = EditableDataType.String,
                    IsRequired = true,
                    Order = 1,
                    Group = "General",
                    ShowInList = true
                },
                new EditableFieldModel()
                {
                    Name = "Description",
                    Description = "A description of the study",
                    Code = "description",
                    Type = EditableDataType.Text,
                    IsRequired = false,
                    Order = 2,
                    Group = "General",
                    ShowInList = true
                },
            };

            model.Fields = fields;

            return await Task.FromResult(new Success<EditableObjectDefinition>(model));
        }

    }
}
