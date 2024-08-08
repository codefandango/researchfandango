using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.Participation;

namespace CodeFandango.Flamenco.Web.Portal.Interfaces
{
    public interface IParticipationService
    {
        Task<Success<ParticipationSetupModel>> GetParticipationSetup(string type, long id);
        Task<Success<ParticipantFieldDefinitionModel>> UpdateOrCreateField(ParticipantFieldDefinitionModel model);
    }
}
