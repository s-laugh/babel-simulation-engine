using System;
using System.Collections.Generic;

namespace maternity_benefits.Storage.EF.Models
{
    public class MaternityBenefitsSimulationResult
    {
        public Guid Id { get; set; }
        public Guid SimulationId { get; set; }
        public List<PersonResult> PersonResults { get; set; }
    }
}