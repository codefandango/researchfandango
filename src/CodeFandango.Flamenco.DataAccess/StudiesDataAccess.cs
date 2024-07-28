using CodeFandango.Flamenco.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeFandango.Flamenco.DataAccess
{
    public class StudiesDataAccess : IObectDataAccess<Study, StudySpec>
    {
        private DataAccess dataAccess;

        public StudiesDataAccess(DataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        public async Task<List<Study>> GetObjects(StudySpec spec)
        {
            return await dataAccess.Database.Studies.ToListAsync();
        }

        public async Task<Study> GetObject(long id)
        {
            return await dataAccess.Database.Studies.FirstOrDefaultAsync(s => s.Id == id) ?? throw new Exception("Object not found");
        }

    }
}