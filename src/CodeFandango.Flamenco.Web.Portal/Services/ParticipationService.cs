using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.DataEntry;
using CodeFandango.Flamenco.Models.Participation;
using CodeFandango.Flamenco.Web.Portal.Interfaces;

namespace CodeFandango.Flamenco.Web.Portal.Services
{
    public class ParticipationService : IParticipationService
    {
        private readonly IDataAccess data;
        private readonly IDataMapper mapper;

        public ParticipationService(IDataAccess data, IDataMapper mapper)
        {
            this.data = data;
            this.mapper = mapper;
        }

        public async Task<Success<ParticipationSetupModel>> GetParticipationSetup(string type, long id)
        {
            var model = new ParticipationSetupModel();
            model.ParticipantFieldEditDefinition = new EditableFormModel
            {
                Name = "Participant Field",
                Fields = [
                    new EditableFieldModel
                    {
                        Name = "Name",
                        Code = "name",
                        IsRequired = true,
                        Order = 1,
                        ShowInList = true,
                        Description = "The name of the participant field",
                        Type = EditableDataType.String,
                        Group = "General",
                    },
                    new EditableFieldModel
                    {
                        Code = "code",
                        Name = "Unique Identifier",
                        IsRequired = true,
                        Order = 2,
                        ShowInList = false,
                        Description = "A unique identifier for the participant field",
                        Type = EditableDataType.Code,
                        Group = "General",
                        FieldReference = "name",
                        UniqueScope = "ParticipantFieldDefinition"
                    },
                    new EditableFieldModel
                    {
                        Name = "Description",
                        Code = "description",
                        IsRequired = false,
                        Order = 3,
                        ShowInList = true,
                        Description = "A description of the participant field",
                        Type = EditableDataType.Text,
                        Group = "General",
                    },
                ]
            };

            if (type.Equals("study", StringComparison.CurrentCultureIgnoreCase))
            {
                model.ParticipationFieldDefinitions = await GetStudyParticipationSetup(id);
            }
            else if (type.Equals("survey", StringComparison.CurrentCultureIgnoreCase))
            {
                var survey = await data.Surveys.GetObject(id);
                model.ParticipationFieldDefinitions.AddRange(await GetStudyParticipationSetup(survey.StudyId));
                model.ParticipationFieldDefinitions.AddRange(await GetSurveyParticipationSetup(id));
            }
            else
            {
                return await Task.FromResult(new Success<ParticipationSetupModel>(false, "Invalid participation type"));
            }

            return new Success<ParticipationSetupModel>(model);
        }

        private async Task<List<ParticipantFieldDefinitionModel>> GetSurveyParticipationSetup(long id)
        {
            var fields = await data.ParticipationSetup.GetSurveyParticipantFields(id);
            var result = fields.Select(x => mapper.Map<ParticipantFieldDefinition, ParticipantFieldDefinitionModel>(x)).ToList();
            return result;
        }

        private async Task<List<ParticipantFieldDefinitionModel>> GetStudyParticipationSetup(long id)
        {
            var fields = await data.ParticipationSetup.GetStudyParticipantFields(id);
            var result = fields.Select(x => mapper.Map<ParticipantFieldDefinition, ParticipantFieldDefinitionModel>(x)).ToList();
            return result;
        }

        public async Task<Success<ParticipantFieldDefinitionModel>> UpdateOrCreateField(ParticipantFieldDefinitionModel model)
        {
            var entity = mapper.Map<ParticipantFieldDefinitionModel, ParticipantFieldDefinition>(model);
            if (entity.Id == 0)
            {
                var result = await data.ParticipantFields.CreateParticipantField(entity);
                var mappedResult = mapper.Map<ParticipantFieldDefinition, ParticipantFieldDefinitionModel>(result);
                return new Success<ParticipantFieldDefinitionModel>(mappedResult);
            }
            else
            {
                var result = await data.ParticipantFields.UpdateParticipantField(entity);
                var mappedResult = mapper.Map<ParticipantFieldDefinition, ParticipantFieldDefinitionModel>(result);
                return new Success<ParticipantFieldDefinitionModel>(mappedResult);
            }
        }

    }
}
