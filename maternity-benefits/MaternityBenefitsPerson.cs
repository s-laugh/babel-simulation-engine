using System;
using System.Collections.Generic;
using System.Linq;

using esdc_simulation_base.Src.Classes;

namespace maternity_benefits
{
    public class MaternityBenefitsPerson : IPerson
    {
        public Guid Id { get; set; }
        public UnemploymentRegion UnemploymentRegion { get; set; }
        public int Age { get; set; }
        public string Flsah { get; set; }
        public List<WeeklyIncome> WeeklyIncome { get; set; }

        public MaternityBenefitsPerson() {
            Id = new Guid();
            UnemploymentRegion = new UnemploymentRegion();
            Age = 0;
            Flsah = "";
            WeeklyIncome = new List<WeeklyIncome>();
        }
    }

    public class WeeklyIncome
    {
        public DateTime StartDate { get; set; }
        public decimal Income { get; set; }
    }

    public class UnemploymentRegion
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Province { get; set; }
        public double UnemploymentRate { get; set; }
        public int BestWeeks { get; set; }
        public string Code { get; set; }

        public UnemploymentRegion() {
            Id = new Guid();
            Name = "";
            Province = "";
            UnemploymentRate = 0;
            BestWeeks = 0;
            Code = "";
        }
    }
    
}
