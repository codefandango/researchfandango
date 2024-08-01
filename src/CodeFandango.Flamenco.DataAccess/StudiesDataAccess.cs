using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.Studies;
using Microsoft.EntityFrameworkCore;

namespace CodeFandango.Flamenco.DataAccess
{
    public class StudiesDataAccess : IObjectDataAccess<Study>
    {
        private DataAccess dataAccess;

        public StudiesDataAccess(DataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<IQueryable<Study>> QueryObjects()
        {
            return await Task.FromResult(dataAccess.Database.Studies);
        }

        public async Task<List<Study>> GetObjects()
        {
            return await dataAccess.Database.Studies.ToListAsync();
        }

        public async Task<Study> GetObject(long id)
        {
            return await dataAccess.Database.Studies.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception("Object not found");
        }

        public async Task<Study> CreateObject(Study study)
        {
            if (dataAccess.Database.Studies.Any(s => s.Id == study.Id))
            {
                throw new Exception("Study already exists");
            }

            dataAccess.Database.Studies.Add(study);
            await dataAccess.Database.SaveChangesAsync();
            return study;
        }

        public async Task<Study> UpdateObject(Study study)
        {
            if (!dataAccess.Database.Studies.Any(s => s.Id == study.Id))
            {
                throw new Exception("Study not found");
            }

            dataAccess.Database.Studies.Update(study);
            await dataAccess.Database.SaveChangesAsync();
            return study;
        }

    }
}