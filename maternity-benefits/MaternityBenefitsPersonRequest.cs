using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Classes;

namespace maternity_benefits
{
    public class MaternityBenefitsPersonRequest : IPersonRequest
    {
        public Guid UnemploymentRegionId { get; set; }
        public int Age { get; set; }
        public string Flsah { get; set; }
        public List<WeeklyIncome> WeeklyIncome { get; set; }
    }

    
}
