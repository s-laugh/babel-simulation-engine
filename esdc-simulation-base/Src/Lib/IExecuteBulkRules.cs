using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public interface IExecuteBulkRules<T, U> 
        where T : ISimulationCase
        where U : IPerson
    {
        Dictionary<Guid, decimal> Execute(T rule, IEnumerable<U> persons);
    }
}