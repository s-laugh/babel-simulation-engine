using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Classes;
using maternity_benefits;

namespace esdc_simulation_api
{
    public class MaternityBenefitsPersonResult {
        public MaternityBenefitsPerson Person { get; set; }
        public decimal BaseAmount { get; set; }
        public decimal VariantAmount { get; set; }
    }

    public class MaternityBenefitsSimulationResult
    {
        public List<MaternityBenefitsPersonResult> PersonResults { get; set; }

        public MaternityBenefitsSimulationResult(SimulationResult result) 
        {
            PersonResults = result.PersonResults.Select(x => {
                return new MaternityBenefitsPersonResult() {
                    VariantAmount = x.VariantAmount,
                    BaseAmount = x.BaseAmount,
                    Person = (MaternityBenefitsPerson)x.Person
                };
            }).ToList();
        }
    }
}