using System;
using System.Collections.Generic;

namespace BenefitsServices.Models
{
    public partial class BenefitsCost
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public float BiWeeklyDeduction { get; set; }
        public float YearlyBenefitsCost { get; set; }

        public virtual Employees Employee { get; set; }

        public static BenefitsCost Create(int vEmployeeId, float vBiWeeklyDeduction, float vYearlyBenefitsCost)
        {
            BenefitsCost result = new BenefitsCost();
            result.EmployeeId = vEmployeeId;
            result.BiWeeklyDeduction = vBiWeeklyDeduction;
            result.YearlyBenefitsCost = vYearlyBenefitsCost;

            return result;

        }
        public void Update(int vEmployeeId, float vBiWeeklyDeduction, float vYearlyBenefitsCost)
        {
            EmployeeId = vEmployeeId;
            BiWeeklyDeduction = vBiWeeklyDeduction;
            YearlyBenefitsCost = vYearlyBenefitsCost;

        }
    }
}
