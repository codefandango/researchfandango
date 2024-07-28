using CodeFandango.Flamenco.Data;

namespace CodeFandango.Flamenco.DataAccess
{
    public interface IObectDataAccess<TObjectType, TSpecEnum>
        where TObjectType : class
        where TSpecEnum : struct
    {
        Task<List<TObjectType>> GetObjects(StudySpec spec);
        Task<TObjectType> GetObject(long id);
    }
}