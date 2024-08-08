using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeFandango.Flamenco.Data
{

    public class FlamencoDb : IdentityDbContext<FlamencoIdentity, FlamencoRole, long>
    {

        public FlamencoDb(DbContextOptions options) : base(options)
        {
        }

        protected FlamencoDb()
        {
        }


        public DbSet<ClientField> ClientFields { get; set; }
        public DbSet<ClientData> Client { get; set; }
        public DbSet<Study> Studies { get; set; }
        public DbSet<Survey> Surveys { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ParticipantFieldDefinition> ParticipantFieldDefinitions { get; set; }
        public DbSet<ParticipantFieldDefinitionCustomer> ParticipantFieldDefinitionCustomers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<ParticipantFieldDefinitionCustomer>()
                .HasKey(x => new { x.ParticipantFieldDefinitionId, x.CustomerId });

            base.OnModelCreating(builder);

        }

    }
}
