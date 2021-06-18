using System;

namespace maternity_benefits.Storage.EF.Models
{
    public class MaternityBenefitsPerson
    {
        public Guid Id { get; set; }
        public int NumBestWeeks { get; set; }
        public decimal AverageIncome { get; set; }
    }
}