using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace maternity_benefits.Storage.EF.Models
{
    [Table("MaternityBenefitsSimulationResult")]
    public class MaternityBenefitsSimulationResult
    {
        public Guid Id { get; set; }
        public Guid SimulationId { get; set; }
        [ForeignKey("SimulationId ")]
        public MaternityBenefitsSimulation Simulation { get; set; }
        public List<MaternityBenefitsPersonResult> PersonResults { get; set; }
        
    }
}