using CodeFandango.Flamenco.Data;
using Microsoft.EntityFrameworkCore;

namespace CodeFandango.Flamenco.DataAccess
{
    public class ParticipantFieldDataAccess
    {
        private DataAccess data;

        public ParticipantFieldDataAccess(DataAccess dataAccess)
        {
            this.data = dataAccess;
        }

        public async Task<ParticipantFieldDefinition> GetParticipantField(long id)
        {
            return await data.Database.ParticipantFieldDefinitions.FirstOrDefaultAsync(x=>x.Id == id) ?? throw new Exception("Field not found.");
        }

        public async Task<List<ParticipantFieldDefinition>> GetParticipantFields()
        {
            return await data.Database.ParticipantFieldDefinitions.ToListAsync();
        }

        public async Task<ParticipantFieldDefinition> CreateParticipantField(ParticipantFieldDefinition field)
        {
            if (data.Database.ParticipantFieldDefinitions.Any(x => x.Id == field.Id))
            {
                throw new Exception("Field already exists");
            }

            data.Database.ParticipantFieldDefinitions.Add(field);
            await data.Database.SaveChangesAsync();
            return field;
        }

        public async Task<ParticipantFieldDefinition> UpdateParticipantField(ParticipantFieldDefinition field)
        {
            if (!data.Database.ParticipantFieldDefinitions.Any(x => x.Id == field.Id))
            {
                throw new Exception("Field not found");
            }

            data.Database.ParticipantFieldDefinitions.Update(field);
            await data.Database.SaveChangesAsync();
            return field;
        }

        public async Task<IQueryable<ParticipantFieldDefinition>> QueryParticipantFields()
        {
            return await Task.FromResult(data.Database.ParticipantFieldDefinitions);
        }

    }
}