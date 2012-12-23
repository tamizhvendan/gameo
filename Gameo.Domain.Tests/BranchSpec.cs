using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class BranchSpec  :EntitySpecBase
    {
        private Branch branch;

        [SetUp]
        public void SetUp()
        {
            branch = new Branch();
        }

        [Test]
        public void Branch_name_should_not_be_empty()
        {
            AssertEntityValidationError(branch, "The Name field is required.");
        }

        [Test]
        public void IsOperating_should_be_true_by_default()
        {
            branch.IsOperating.ShouldBeTrue();
        }
    }
}