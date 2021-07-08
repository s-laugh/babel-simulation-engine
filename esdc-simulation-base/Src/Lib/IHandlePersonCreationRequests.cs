using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Lib
{
    public interface IHandlePersonCreationRequests<T>
    {
        void Handle(IEnumerable<T> persons);
    }
}