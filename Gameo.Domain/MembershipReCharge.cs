using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gameo.Domain
{
    public class MembershipReCharge : IValidatableObject
    {
        public DateTime RechargedOn { get; set; }
        public decimal Price { get; set; }
        public int Hours { get; set; }
        public string BranchName { get; set; }

        public MembershipReCharge()
        {
            RechargedOn = DateTime.Now.ToIST();
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price <= 0)
            {
                yield return new ValidationResult("Price should be greater than zero.");
            }

            if (Hours <= 0)
            {
                yield return new ValidationResult("Hours should be greater than zero.");
            }
        }
    }
}