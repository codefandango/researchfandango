using CodeFandango.Flamenco.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.DataAccess
{
    public class DataAccess : IDataAccess
    {
        private readonly FlamencoDb db;

        public FlamencoDb Database => db;

        public DataAccess(FlamencoDb db)
        {
            this.db = db;
            this.Clients = new ClientDataAccess(this);
            this.Studies = new StudiesDataAccess(this);
            this.Surveys = new SurveysDataAccess(this);
        }

        public IObjectDataAccess<T> GetObjectAccessor<T>() where T : class
        {

            foreach (var property in this.GetType().GetProperties())
            {
                var attr = property.GetCustomAttribute<ObjectAccessorAttribute>();
                if (attr != null && attr.ObjectType == typeof(T))
                {
                    return property.GetValue(this) as IObjectDataAccess<T> ?? throw new Exception("No object accessor found.");
                }
            }

            throw new Exception($"No object accessor found for type {typeof(T).Name}");

        }

        public ClientDataAccess Clients { get; set; }

        [ObjectAccessor(typeof(Study))]
        public StudiesDataAccess Studies { get; set; }

        [ObjectAccessor(typeof(Survey))]
        public SurveysDataAccess Surveys { get; set; }

    }
}
