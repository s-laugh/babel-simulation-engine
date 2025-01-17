﻿using System;

namespace esdc_simulation_classes.MaternityBenefits
{
    public class CaseRequest
    {
        public decimal MaxWeeklyAmount { get; set; }
        public double Percentage { get; set; }
        public int NumWeeks { get; set; }

        public CaseRequest() {
            Percentage = 0;
            MaxWeeklyAmount = 0;
            NumWeeks = 0;
        }
    }
}
