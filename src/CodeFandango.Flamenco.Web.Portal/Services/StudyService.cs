using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.DataEntry;
using CodeFandango.Flamenco.Models.Studies;
using CodeFandango.Flamenco.Web.Portal.Interfaces;

namespace CodeFandango.Flamenco.Web.Portal.Services
{
    internal class StudyService : IStudyService
    {
        private readonly IDataAccess da;
        private readonly IDataMapper mapper;

        public StudyService(IDataAccess da, IDataMapper mapper)
        {
            this.da = da;
            this.mapper = mapper;
        }

        public async Task<Success<EditableFieldCollection>> GetDefinition()
        {
            var model = new EditableFieldCollection()
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
                    Type = EditableDataType.String,
                    IsRequired = false,
                    Order = 2,
                    Group = "General",
                    ShowInList = true
                },
            };

            return await Task.FromResult(new Success<EditableFieldCollection>(model));
        }

        public async Task<Success<List<StudyModel>>> GetStudies()
        {
            try
            {
                var studies = await da.Studies.GetObjects(StudySpec.None);
                var mappedList = studies.Select(s => mapper.Map<Study, StudyModel>(s)).ToList();
                return new Success<List<StudyModel>>(mappedList);
            }
            catch (Exception ex)
            {
                return new Success<List<StudyModel>>(false, ex.Message);
            }
        }

        public async Task<Success<StudyModel>> GetStudy(int id)
        {
            try
            {
                var study = await da.Studies.GetObject(id);
                var model = mapper.Map<Study, StudyModel>(study);
                return new Success<StudyModel>(model);
            }
            catch (Exception ex)
            {
                return new Success<StudyModel>(false, ex.Message);
            }
        }
    }
}
