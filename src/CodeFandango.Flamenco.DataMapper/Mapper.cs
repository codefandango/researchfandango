using AutoMapper;
using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.Models.Studies;

namespace CodeFandango.Flamenco.Models
{
    public class DataMapper : IDataMapper
    {
        public DataMapper() 
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Study, StudyModel>().ReverseMap();
            });

            mapper = config.CreateMapper();
        }

        private IMapper mapper;

        public TDest Map<TSource, TDest>(TSource source)
        {
            return mapper.Map<TSource, TDest>(source);
        }
    }
}
