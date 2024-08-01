using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.Models;

namespace CodeFandango.Flamenco.DataAccess
{
    public interface IObjectDataAccess<TObjectType>
        where TObjectType : class
    {
        Task<TObjectType> GetObject(long id);
        Task<TObjectType> CreateObject(TObjectType study);
        Task<TObjectType> UpdateObject(TObjectType study);
        Task<IQueryable<TObjectType>> QueryObjects();
        Task<List<TObjectType>> GetObjects();
    }
}