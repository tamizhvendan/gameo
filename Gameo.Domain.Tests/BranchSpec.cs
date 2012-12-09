using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class BranchSpec
    {
        [Test]
        public void Branch_name_should_not_be_empty()
        {
            var branch = new Branch();
            var validationContext = new ValidationContext(branch, null, null);

            var validationException = Assert.Throws<ValidationException>(() => Validator.ValidateObject(branch, validationContext));
            validationException.ValidationResult.ErrorMessage.ShouldEqual("The Name field is required.");
        }

        [Test]
        public void IsOperating_should_be_true_by_default()
        {
            var branch = new Branch();

            branch.IsOperating.ShouldBeTrue();
        }
    }
}