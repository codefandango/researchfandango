using CodeFandango.Flamenco.Data;
using Microsoft.EntityFrameworkCore;
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
            this.Customers = new CustomerDataAccess(this);
            this.ParticipationSetup = new ParticipationSetupDataAccess(this);
            this.ParticipantFields = new ParticipantFieldDataAccess(this);

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

        private IQueryable<UniquelyCodedEntity> GetDbSetByName(DbContext context, string entityName)
        {
            // Find the entity type by name
            var entityType = context.Model.GetEntityTypes()
                                 .FirstOrDefault(t => t.ClrType.Name == entityName);

            if (entityType == null)
                throw new InvalidOperationException($"Entity type '{entityName}' not found in the DbContext.");

            // Use reflection to get the DbSet property
            var dbSetProperty = context.GetType()
                                       .GetProperties()
                                       .FirstOrDefault(p => p.PropertyType == typeof(DbSet<>).MakeGenericType(entityType.ClrType));

            if (dbSetProperty == null)
                throw new InvalidOperationException($"DbSet for entity type '{entityName}' not found in the DbContext.");

            // Get the value of the DbSet property
            var dbSet = dbSetProperty.GetValue(context);

            return dbSet as IQueryable<UniquelyCodedEntity> ?? throw new InvalidOperationException("Entity type not found.");
        }

        public async Task<bool> UniqueCodeExists(string scope, string pascalCase)
        {
            var set = GetDbSetByName(db, scope);
            if (set == null)
                throw new InvalidOperationException($"Entity type '{scope}' not found in the DbContext.");

            var exists = await set.AnyAsync(x => x.Code == pascalCase);
            return exists;
        }

        public ClientDataAccess Clients { get; set; }

        [ObjectAccessor(typeof(Study))]
        public StudiesDataAccess Studies { get; set; }

        [ObjectAccessor(typeof(Survey))]
        public SurveysDataAccess Surveys { get; set; }

        [ObjectAccessor(typeof(Customer))]
        public CustomerDataAccess Customers { get; set; }
        
        public ParticipationSetupDataAccess ParticipationSetup { get; set; }
        public ParticipantFieldDataAccess ParticipantFields { get; set; }

    }
}
