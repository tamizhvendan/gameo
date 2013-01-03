using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Gameo.Domain
{
    public class Game : Entity, IValidatableObject
    {
        public double HoursPlayed
        {
            get { return Math.Round((OutTime - InTime).TotalHours, 1); }
        }

        [Required(ErrorMessage = "Customer Name is required.")]
        public string CustomerName { get; set; }
        public string CustomerContactNumber { get; set; }
        public DateTime InTime { get; set; }
        public DateTime OutTime { get; set; }
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Console Name is required.")]
        public string ConsoleName { get; set; }
        public GamePaymentType GamePaymentType { get; set; }
        public PackageType PackageType { get; set; }
        public Game()
        {
            InTime = DateTime.Now;
            OutTime = InTime.AddHours(1);
            GamePaymentType = GamePaymentType.OneTime;
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

            var differenceBetweenInTimeAndOutTimeInMinutes = ((int) OutTime.Subtract(InTime).TotalMinutes);
            if (differenceBetweenInTimeAndOutTimeInMinutes == 0 || differenceBetweenInTimeAndOutTimeInMinutes % 30 != 0)
            {
                yield return new ValidationResult("Difference between In Time and Out Time should be in multiples of half-hour.");
            }

            if (GamePaymentType == GamePaymentType.OneTime && Price <= 0)
            {
                yield return new ValidationResult("Price should be greater than zero.");

            }

            if (InTime > DateTime.Now)
            {
                yield return new ValidationResult("In Time should not be greater than current time.");
            }
        }
    }
}