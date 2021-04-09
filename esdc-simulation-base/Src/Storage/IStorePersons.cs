
using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Storage
{
    public interface IStorePersons<T> where T : IPerson
    {
        IEnumerable<T> GetAllPersons();
    }
}