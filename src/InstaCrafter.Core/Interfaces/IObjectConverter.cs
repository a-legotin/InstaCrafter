using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaCrafter.Core.Interfaces
{
    interface IObjectConverter<T, TT>
    {
        TT SourceObject { get; set; }
        T Convert();
    }
}
