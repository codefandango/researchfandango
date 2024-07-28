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

    }
}
