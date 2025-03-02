﻿using AutoMapper;
using CodeFandango.Flamenco.Abstractions;
using CodeFandango.Flamenco.Data;
using CodeFandango.Flamenco.Models.Customers;
using CodeFandango.Flamenco.Models.Participation;
using CodeFandango.Flamenco.Models.Studies;
using CodeFandango.Flamenco.Models.Surveys;

namespace CodeFandango.Flamenco.Models
{
    public class DataMapper : IDataMapper
    {
        public DataMapper() 
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Study, StudyModel>().ReverseMap();
                cfg.CreateMap<Survey, SurveyModel>().ReverseMap();
                cfg.CreateMap<Customer, CustomerModel>().ReverseMap();
                cfg.CreateMap<ParticipantFieldDefinition, ParticipantFieldDefinitionModel>().ReverseMap();
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
