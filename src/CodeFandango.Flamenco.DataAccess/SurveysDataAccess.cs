using CodeFandango.Flamenco.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeFandango.Flamenco.DataAccess
{
    public class SurveysDataAccess : IObjectDataAccess<Survey>
    {
        private IDataAccess data;

        public SurveysDataAccess(IDataAccess data)
        {
            this.data = data;
        }

        public async Task<Survey> CreateObject(Survey survey)
        {
            if (data.Database.Surveys.Any(s => s.Id == survey.Id))
            {
                throw new Exception("Survey already exists");
            }

            data.Database.Surveys.Add(survey);
            await data.Database.SaveChangesAsync();
            return survey;
        }

        public async Task<Survey> GetObject(long id)
        {
            return await data.Database.Surveys.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception("Surey not found.");
        }

        public async Task<List<Survey>> GetObjects()
        {
            return await data.Database.Surveys.ToListAsync();
        }

        public Task<IQueryable<Survey>> QueryObjects()
        {
            throw new NotImplementedException();
        }

        public Task<Survey> UpdateObject(Survey study)
        {
            throw new NotImplementedException();
        }
    }
}