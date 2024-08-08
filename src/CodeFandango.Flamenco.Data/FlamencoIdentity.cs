using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Data
{
    public class FlamencoIdentity : IdentityUser<long>
    {
        public long? CustomerId { get; set; }
        public Customer? Customer { get; set; }
    }
}
