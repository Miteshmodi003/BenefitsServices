using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BenefitsServices.Models;
using Microsoft.AspNetCore.Routing.Constraints;

namespace BenefitsServices.BusinessRulesUtil
{
    public static class CalculationUtil
    {
        private const float PayCheckAmount = 2000F;
        private const int PaychecksPerYear = 26;
        private const float AnnualBenefitsCostForEmployee = 1000F;
        private const float AnnualBenefitsCostForDependents = 500F;
        private const float DiscountRate = 0.1F;
        private const float NonDiscountPercentage = 1F;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="objEmployee"></param>
        public static void SetDeductionAmounts(Employees objEmployee)
        {
            if (!string.IsNullOrEmpty(objEmployee?.FirstName))
            {
                var totalAnnualBenefitsCost = (bool)objEmployee.FirstName.StartsWith('A')
                    ? AnnualBenefitsCostForEmployee * (NonDiscountPercentage - DiscountRate)
                    : AnnualBenefitsCostForEmployee;

                var totalDependentsCount = objEmployee.Dependents.Count;
                if (totalDependentsCount > 0)
                {
                    var discountedDependentsCount = objEmployee.Dependents.Count(dependent => dependent.FirstName.StartsWith('A'));
                    totalAnnualBenefitsCost += TotalBenefitsCostForDependents(totalDependentsCount, discountedDependentsCount);
                }

                objEmployee.BenefitsCost = new BenefitsCost
                {
                    YearlyBenefitsCost = (float)Math.Round(totalAnnualBenefitsCost),
                    BiWeeklyDeduction = (float)Math.Round(totalAnnualBenefitsCost / 26)
                };

                objEmployee.AnnualCompensation = PayCheckAmount * PaychecksPerYear;
            }
        }

        /// <summary>
        /// Calculates Total Annual Benefits Cost for All Dependents
        /// </summary>
        /// <param name="totalNumberOfDependents">Total Number of Dependents</param>
        /// <param name="discountedDependentsCount">Dependents Eligible for Discounts</param>
        /// <returns>Returns deduction cost for all dependents</returns>
        private static float TotalBenefitsCostForDependents(float totalNumberOfDependents = 0, float discountedDependentsCount = 0)
        {
            return (totalNumberOfDependents - discountedDependentsCount) * (AnnualBenefitsCostForDependents) + discountedDependentsCount * (AnnualBenefitsCostForDependents * NonDiscountPercentage - DiscountRate);
        }
    }
}
