
using CodeFandango.Flamenco.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeFandango.Flamenco.DataAccess
{
    public class ParticipationSetupDataAccess
    {
        private DataAccess dataAccess;

        public ParticipationSetupDataAccess(DataAccess data)
        {
            this.dataAccess = data;
        }

        public IQueryable<ParticipantFieldDefinition> QueryStudyParticipantFields(long id)
        {
            return dataAccess.Database.ParticipantFieldDefinitions.Where(pfd => pfd.StudyId == id);
        }

        public IQueryable<ParticipantFieldDefinition> QuerySurveyParticipantFields(long id)
        {
            return dataAccess.Database.ParticipantFieldDefinitions.Where(pfd => pfd.SurveyId == id);
        }

        public IQueryable<ParticipantFieldDefinition> QueryParticipantFields(long surveyId)
        {
            return dataAccess.Database.ParticipantFieldDefinitions.Where(pfd => pfd.SurveyId == surveyId
                || (pfd.Study != null && pfd.Study.Surveys.Any(y => y.Id == surveyId)));
        }

        public async Task<List<ParticipantFieldDefinition>> GetStudyParticipantFields(long id)
        {
            return await this.QueryStudyParticipantFields(id).ToListAsync();
        }

        public async Task<List<ParticipantFieldDefinition>> GetSurveyParticipantFields(long id)
        {
            return await this.QuerySurveyParticipantFields(id).ToListAsync();
        }

        public async Task<List<ParticipantFieldDefinition>> GetParticipantFields(long surveyId)
        {
            return await this.QueryParticipantFields(surveyId).ToListAsync();
        }
    }
}