using CodeFandango.Flamenco.Data;
using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        public ClientDataAccess Clients { get; set; }

        public StudiesDataAccess Studies { get; set; }

    }
}
