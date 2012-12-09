using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class GameConsoleSpec : EntitySpecBase
    {
        private GameConsole gameConsole;

        [SetUp]
        public void SetUp()
        {
            gameConsole = new GameConsole();
        }

        [Test]
        public void GameConsoleStatus_should_be_working_by_default()
        {
            gameConsole.GameConsoleStatus.ShouldEqual(GameConsoleStatus.Working);
        }

        [Test]
        public void Name_should_not_be_empty()
        {
           Validate(gameConsole, "The Name field is required.");
        }

        [Test]
        public void BranchName_should_not_be_empty()
        {
            gameConsole.Name = "foo";
            Validate(gameConsole, "The BranchName field is required.");
        }
    }
}