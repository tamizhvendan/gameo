using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gameo.Domain
{
    public class DailySaleDetails : Entity, IValidatableObject
    {
        public DateTime DateTime { get; set; }
        public decimal TotalCollection { get; set; }
        public decimal EbMeterReading { get; set; }
        public string BranchName { get; set; }
        public decimal AmountInHand { get; set; }

        public DailySaleDetails()
        {
            var currentTime = DateTime.UtcNow.ToIST().Date;

            DateTime = new DateTime(currentTime.Year, currentTime.Month, currentTime.Day);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EbMeterReading <= 0)
            {
                yield return new ValidationResult("EB Meter Reading should be greater than zero.", new[] { "EbMeterReading" });
            }

            if (AmountInHand <= 0)
            {
                yield return new ValidationResult("Amount in hand should be greater than zero.", new[] { "AmountInHand" });
            }

            if (TotalCollection < 0)
            {
                yield return new ValidationResult("Total Collection should be greater than or equal to zero.", new[] { "TotalCollection" });
            }
        }
    }
}