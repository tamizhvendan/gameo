using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class GamingConsoleSpec : EntitySpecBase
    {
        private GamingConsole gamingConsole;

        [SetUp]
        public void SetUp()
        {
            gamingConsole = new GamingConsole();
        }

        [Test]
        public void Status_should_be_working_by_default()
        {
            gamingConsole.Status.ShouldEqual(Status.Working);
        }

        [Test]
        public void Name_should_not_be_empty()
        {
           Validate(gamingConsole, "The Name field is required.");
        }

        [Test]
        public void BranchName_should_not_be_empty()
        {
            gamingConsole.Name = "foo";
            Validate(gamingConsole, "The BranchName field is required.");
        }
    }
}