using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    public abstract class EntitySpecBase
    {
        protected void Validate<T>(T entity, string expectedErrorMessage) where T : Entity
        {
            var validationContext = new ValidationContext(entity, null, null);
            var validationException = Assert.Throws<ValidationException>(() => Validator.ValidateObject(entity, validationContext));
            validationException.ValidationResult.ErrorMessage.ShouldEqual(expectedErrorMessage);
        }
    }
}