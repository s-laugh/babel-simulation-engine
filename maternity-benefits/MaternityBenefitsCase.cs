using System;
using System.Collections.Generic;

using esdc_simulation_base.Src.Classes;

namespace maternity_benefits
{
    public class MaternityBenefitsCase : MaternityBenefitsCaseRequest, ISimulationCase
    {
        public Guid Id { get; set; }
        public Dictionary<Guid, UnemploymentRegion> RegionDict { get; set; }

        public MaternityBenefitsCase() : base() {
            RegionDict = new Dictionary<Guid, UnemploymentRegion>();
        }

        public MaternityBenefitsCase(MaternityBenefitsCaseRequest request, Dictionary<Guid, UnemploymentRegion> regionDict) {
            Id = Guid.NewGuid();
            NumWeeks = request.NumWeeks;
            MaxWeeklyAmount = request.MaxWeeklyAmount;
            Percentage = request.Percentage;
            RegionDict = regionDict;  
        }
    }
}
