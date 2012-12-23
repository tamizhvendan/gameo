using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    public abstract class EntitySpecBase
    {
        protected void AssertEntityValidationError<T>(T entity, string expectedErrorMessage) where T : Entity
        {
            var validationContext = new ValidationContext(entity, null, null);
            var validationException = Assert.Throws<ValidationException>(() => Validator.ValidateObject(entity, validationContext));
            validationException.ValidationResult.ErrorMessage.ShouldEqual(expectedErrorMessage);
        }

        protected void AssertZeroValidationError<T>(T entity) where  T : Entity
        {
            var validationContext = new ValidationContext(entity, null, null);
            var validaitonResults = new List<ValidationResult>();

            Validator.TryValidateObject(entity, validationContext, validaitonResults);
            validaitonResults.Count.ShouldEqual(0);
        }
    }
}