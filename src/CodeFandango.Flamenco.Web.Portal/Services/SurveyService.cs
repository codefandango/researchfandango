using AutoMapper;
using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.DataEntry;
using CodeFandango.Flamenco.Models.Surveys;
using CodeFandango.Flamenco.Web.Portal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CodeFandango.Flamenco.Web.Portal.Services
{
    public class SurveyService : ObjectEditorService<Survey, SurveyModel>, ISurveyService
    {
        public SurveyService(IDataAccess data, IDataMapper mapper) : base(data, mapper)
        {
        }

        public override async Task<Success<EditableObjectDefinition>> GetDefinition()
        {
            var model = new EditableObjectDefinition
            {
                Name= "Survey",
                Fields =
                [
                    new EditableFieldModel
                    {
                        Name = "Name",
                        Code = "name",
                        IsRequired = true,
                        Order = 1,
                        ShowInList = true,
                        Description = "The name of the survey",
                        Type = EditableDataType.String,
                        Group = "General",
                    },
                    new EditableFieldModel
                    {
                        Name = "Description",
                        Code = "description",
                        IsRequired = false,
                        Order = 2,
                        ShowInList = true,
                        Description = "A description of the survey",
                        Type = EditableDataType.Text,
                        Group = "General",
                    },
                    new EditableFieldModel
                    {
                        Code = "studyId",
                        Name = "Study",
                        IsRequired = true,
                        Order = 3,
                        ShowInList = true,
                        Description = "The study this survey is associated with",
                        Type = EditableDataType.Reference,
                        Group = "General",
                        ObjectReference = "study"
                    }
                ],
                Actions =
                [
                    new ObjectActionDefinition
                    {
                        Name = "Participation",
                        Code = "participation",
                        Order = 1,
                        Icon = "fas fa-users",
                        Action = "navigate",
                        Path = "/admin/participation/?surveyId={id}",
                        Scope = ObjectActionScope.Edit
                    },
                    new ObjectActionDefinition
                    {
                        Name = "Questions",
                        Code = "questions",
                        Order = 2,
                        Icon = "fas fa-question",
                        Action = "navigate",
                        Path = "/admin/questions/{id}",
                        Scope = ObjectActionScope.Edit
                    }
                ]
            };

            var studyQuery = await data.Studies.QueryObjects();
            var studyList = await studyQuery.Select(x => new ReferenceObject { Id = x.Id, Name = x.Name }).ToListAsync();
            model.References.Add("study", studyList);

            return new Success<EditableObjectDefinition>(model);
        }
    }
}
