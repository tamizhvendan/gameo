using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class GameConsoleRepositorySpec : RepositoryTestBase<GamingConsole>
    {
        private GamingConsoleRepository gamingConsoleRepository;

        [SetUp]
        public void SetUp()
        {
            gamingConsoleRepository = new GamingConsoleRepository();
        }

        [Test]
        public void Adds_new_GameConsole()
        {
            var gameConsoleToAdd = CreateGameConsole("foo", "bar");
            
            gamingConsoleRepository.Add(gameConsoleToAdd);

            AssertNewlyAddedEntity(actualGameConsoleStored =>
                                       {
                                           actualGameConsoleStored.Name.ShouldEqual(gameConsoleToAdd.Name);
                                           actualGameConsoleStored.BranchName.ShouldEqual(gameConsoleToAdd.BranchName);
                                       });
        }

        [Test]
        public void Lists_all_GameConsoles()
        {
            AddEntityToDatabase(CreateGameConsole("Console1", "Branch1"));
            AddEntityToDatabase(CreateGameConsole("Console2", "Branch2"));

            var gameConsoles = gamingConsoleRepository.All.ToList();

            gameConsoles.Count().ShouldEqual(2);
            gameConsoles.Any(console => console.BranchName == "Branch1" && console.Name == "Console1").ShouldBeTrue();
            gameConsoles.Any(console => console.BranchName == "Branch2" && console.Name == "Console2").ShouldBeTrue();
        }

        private static GamingConsole CreateGameConsole(string gameConsoleName, string branchName)
        {
            return new GamingConsole { Name = gameConsoleName, BranchName = branchName };
        }

        [Test]
        public void Gets_GameConsole_by_guid()
        {
            var gameConsole = CreateGameConsole("foo", "bar");
            AddEntityToDatabase(gameConsole);

            var acutalGameConsole = gamingConsoleRepository.GetById(gameConsole.Id);

            acutalGameConsole.Id.ShouldEqual(gameConsole.Id);
            acutalGameConsole.Name.ShouldEqual(gameConsole.Name);
            acutalGameConsole.BranchName.ShouldEqual(gameConsole.BranchName);
        }

        [Test]
        public void Deletes_GameConsole_by_guid()
        {
            var gameConsole = CreateGameConsole("foo", "bar");
            AddEntityToDatabase(gameConsole);

            gamingConsoleRepository.Delete(gameConsole.Id);

            AssertDeletedEntity();
        }

        [Test]
        public void Updates_GameConsole()
        {
            var gameConsole = CreateGameConsole("foo", "bar");
            AddEntityToDatabase(gameConsole);
            gameConsole.Name = "bar";

            gamingConsoleRepository.Update(gameConsole);

            AssertUpdatedEntity(gameConsole.Id, updatedGameConsole =>
                                                    {
                                                        updatedGameConsole.Name.ShouldEqual(gameConsole.Name);
                                                        updatedGameConsole.Id.ShouldEqual(gameConsole.Id);
                                                        updatedGameConsole.BranchName.ShouldEqual(gameConsole.BranchName);
                                                    });
        }

        [Test]
        public void Checks_the_existance_of_gameconsole_name_within_the_branch_with_case_ignored()
        {
            var gameConsole1 = CreateGameConsole("Console1", "Branch1");
            var gameConsole2 = CreateGameConsole("Console2", "Branch2");
            AddEntityToDatabase(gameConsole1, gameConsole2);

            var isGameConsole1Exists = gamingConsoleRepository.IsConsoleNameExists("Console1".ToUpperInvariant(), "Branch1".ToUpperInvariant());
            var isGameConsole2Exists = gamingConsoleRepository.IsConsoleNameExists("Console2".ToUpperInvariant(), "Branch2".ToUpperInvariant());

            isGameConsole1Exists.ShouldBeTrue();
            isGameConsole2Exists.ShouldBeTrue();
        }

        [Test]
        public void Checks_the_non_existance_of_gameconsole_name_within_the_branch_with_case_ignored()
        {
            var gameConsole1 = CreateGameConsole("Console1", "Branch2");
            var gameConsole2 = CreateGameConsole("Console2", "Branch1");
            AddEntityToDatabase(gameConsole1, gameConsole2);

            var isGameConsole1Exists = gamingConsoleRepository.IsConsoleNameExists("Console1".ToUpperInvariant(), "Branch1".ToUpperInvariant());
            var isGameConsole2Exists = gamingConsoleRepository.IsConsoleNameExists("Console2".ToUpperInvariant(), "Branch2".ToUpperInvariant());

            isGameConsole1Exists.ShouldBeFalse();
            isGameConsole2Exists.ShouldBeFalse();
        }
    }
}