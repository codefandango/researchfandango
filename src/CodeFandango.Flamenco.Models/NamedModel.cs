﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Models
{
    public class NamedModel
    {
        public long Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }

    public class UniquelyCodedModel : NamedModel
    {
        public required string Code { get; set; }
    }
}
