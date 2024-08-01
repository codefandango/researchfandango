using CodeFandango.Flamenco.Data;

namespace CodeFandango.Flamenco.DataAccess
{
    public interface IDataAccess
    {
        FlamencoDb Database { get; }
        ClientDataAccess Clients { get; set; }
        StudiesDataAccess Studies { get; set; }
        SurveysDataAccess Surveys { get; set; }

        IObjectDataAccess<T> GetObjectAccessor<T>() where T : class;
    }
}