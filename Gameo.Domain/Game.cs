using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gameo.Domain
{
    public class Game : Entity, IValidatableObject
    {
        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }
        public string CustomerContactNumber { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public decimal Price { get; set; }
        public string ConsoleName { get; set; }
        public GamePaymentType GamePaymentType { get; set; }
        public bool IsPackage { get; set; }

        public Game()
        {
            InTime = DateTime.Now;
            OutTime = InTime.AddHours(1);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (InTime == DateTime.MinValue)
            {
                yield return new ValidationResult("In Time is required.");
            }

            if (OutTime == DateTime.MinValue)
            {
                yield return new ValidationResult("Out Time is required.");
            }

            if (OutTime <= InTime)
            {
                yield return new ValidationResult("Out time should be greater than in time.");
            }

            if (((int)OutTime.Subtract(InTime).TotalMinutes) % 30 != 0)
            {
                yield return new ValidationResult("Difference between In Time and Out Time should be in multiples of half-hour.");
            }

            if (Price <= 0)
            {
                yield return new ValidationResult("Price should be greater than zero.");

            }
        }
    }
}