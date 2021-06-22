using System;

namespace maternity_benefits.Storage.EF.Models
{
    // TODO: Need to fix the decimals on all EF Models
    public class MaternityBenefitsPerson
    {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string Flsah { get; set; }
        public decimal AverageIncome { get; set; }
    }
}