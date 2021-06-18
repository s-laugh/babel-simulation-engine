using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Classes;

namespace maternity_benefits
{
    public class MaternityBenefitsPersonRequest : IPersonRequest
    {
        public int Age { get; set; }
        public string Flsah { get; set; }
        public decimal AverageIncome { get; set; }
    }

    
}
