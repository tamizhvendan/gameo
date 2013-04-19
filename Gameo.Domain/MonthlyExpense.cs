using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gameo.Domain
{
    public class MonthlyExpense : Entity, IValidatableObject
    {
        public decimal SalaryPaid { get; set; }

        [Required(ErrorMessage = "Branch Name is required.")]
        public string BranchName { get; set; }
        
        public decimal EbPayment { get; set; }
        public decimal InternetBill { get; set; }
        public decimal Rent { get; set; }
        public decimal OtherExpenses { get; set; }
        public string Description { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }

        public decimal TotalExpenses
        {
            get { return EbPayment + SalaryPaid + InternetBill + Rent + OtherExpenses; }
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (SalaryPaid <= 0)
            {
                yield return new ValidationResult("Salary Paid should be more than zero.", new[] { "SalaryPaid" });
            }    
        }
    }
}