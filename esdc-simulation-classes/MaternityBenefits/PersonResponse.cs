using System;

namespace esdc_simulation_classes.MaternityBenefits
{
    public class PersonResponse {
        public Guid Id { get; set; }
        public int Age { get; set; }
        public string SpokenLanguage { get; set; }
        public string EducationLevel { get; set; }
        public string Province { get; set; }
        public decimal AverageIncome { get; set; }
    }
}