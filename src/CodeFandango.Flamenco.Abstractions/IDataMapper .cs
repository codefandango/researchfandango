using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFandango.Flamenco.Abstractions
{
    public interface IDataMapper
    {
        TDest Map<TSource, TDest>(TSource source);
    }
}
