using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.Models.DataEntry;

namespace CodeFandango.Flamenco.Web.Portal.Interfaces
{
    public interface IObjectEditorService<T, TModel>
    {
        Task<Success<TModel>> CreateOrUpdateObject(TModel model);
        abstract Task<Success<EditableObjectDefinition>> GetDefinition();
        Task<Success<TModel>> GetObject(long id);
        Task<Success<List<TModel>>> GetObjects();
    }
}