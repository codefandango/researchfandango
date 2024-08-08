using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Data
{
    public class UniquelyCodedEntity : NamedEntity
    {
        public required string Code { get; set; }
    }
}
