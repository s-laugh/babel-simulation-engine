using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace esdc_simulation_base.Src.Rules
{
    public interface IRulesEngine
    {
        T Execute<T>(string endpoint, object rulesRequest);
    }
}