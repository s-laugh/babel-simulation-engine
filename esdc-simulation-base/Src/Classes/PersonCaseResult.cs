using System;
using System.Collections.Generic;


namespace esdc_simulation_base.Src.Classes
{
    public class PersonCaseResult
    {
        public IPerson Person { get; set; }

        // This may need to be a generic
        public decimal Amount { get; set; }

    }
}