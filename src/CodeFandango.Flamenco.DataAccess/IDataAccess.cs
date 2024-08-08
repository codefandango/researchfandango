using CodeFandango.Flamenco.Data;

namespace CodeFandango.Flamenco.DataAccess
{
    public interface IDataAccess
    {
        FlamencoDb Database { get; }
        ClientDataAccess Clients { get; set; }
        StudiesDataAccess Studies { get; set; }
        SurveysDataAccess Surveys { get; set; }
        ParticipationSetupDataAccess ParticipationSetup { get; set; }
        ParticipantFieldDataAccess ParticipantFields { get; set; }

        IObjectDataAccess<T> GetObjectAccessor<T>() where T : class;
        Task<bool> UniqueCodeExists(string scope, string pascalCase);
    }
}