using AutoMapper;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.Models.Studies;
using CodeFandango.Flamenco.Models;
using CodeFandango.Flamenco.DataAccess;
using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Web.Portal.Interfaces;
using CodeFandango.Flamenco.Models.DataEntry;

namespace CodeFandango.Flamenco.Web.Portal.Services
{
    public abstract class ObjectEditorService<T, TModel> : IObjectEditorService<T, TModel> 
        where T: class where TModel: NamedModel
    {
        protected readonly IDataAccess data;
        protected readonly IDataMapper mapper;

        public ObjectEditorService(IDataAccess da, IDataMapper mapper)
        {
            this.data = da;
            this.mapper = mapper;
        }

        public virtual async Task<Success<List<TModel>>> GetObjects()
        {
            try
            {
                var accessor = data.GetObjectAccessor<T>();
                var objects = await accessor.GetObjects();
                var mappedList = objects.Select(s => mapper.Map<T, TModel>(s)).ToList();
                return new Success<List<TModel>>(mappedList);
            }
            catch (Exception ex)
            {
                return new Success<List<TModel>>(false, ex.Message);
            }
        }

        public virtual async Task<Success<TModel>> GetObject(int id)
        {
            try
            {
                var accessor = data.GetObjectAccessor<T>();
                var obj = await accessor.GetObject(id);
                var model = mapper.Map<T, TModel>(obj);
                return new Success<TModel>(model);
            }
            catch (Exception ex)
            {
                return new Success<TModel>(false, ex.Message);
            }
        }

        public abstract Task<Success<EditableObjectDefinition>> GetDefinition();

        public virtual async Task<Success<TModel>> CreateOrUpdateObject(TModel model)
        {
            if (model.Id == 0)
            {
                return await CreateObject(model);
            }
            else
            {
                return await UpdateObject(model);
            }
        }

        protected virtual async Task<Success<TModel>> UpdateObject(TModel model)
        {
            throw new NotImplementedException();
        }

        protected virtual async Task<Success<TModel>> CreateObject(TModel model)
        {
            try
            {
                var newEntity = mapper.Map<TModel, T>(model);
                var accessor = data.GetObjectAccessor<T>();
                var result = await accessor.CreateObject(newEntity);
                if (result == null)
                {
                    return new Success<TModel>(false, "Failed to create object.");
                }

                var updatedModel = mapper.Map<T, TModel>(result);
                return new Success<TModel>(updatedModel);
            }
            catch (Exception ex)
            {
                return new Success<TModel>(false, ex.Message);
            }
        }
    }
}