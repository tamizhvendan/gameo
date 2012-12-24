using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class GamingConsoleRepositorySpec : RepositorySpecBase<GamingConsole>
    {
        private GamingConsoleRepository gamingConsoleRepository;

        [SetUp]
        public void SetUp()
        {
            gamingConsoleRepository = new GamingConsoleRepository();
        }

        [Test]
        public void Adds_new_GamingConsole()
        {
            var gamingConsole = CreateGamingConsole("foo", "bar");
            
            gamingConsoleRepository.Add(gamingConsole);

            AssertNewlyAddedEntity(actualGameConsoleStored =>
                                       {
                                           actualGameConsoleStored.Name.ShouldEqual(gamingConsole.Name);
                                           actualGameConsoleStored.BranchName.ShouldEqual(gamingConsole.BranchName);
                                       });
        }

        [Test]
        public void Lists_all_GamingConsoles()
        {
            AddEntityToDatabase(CreateGamingConsole("Console1", "Branch1"));
            AddEntityToDatabase(CreateGamingConsole("Console2", "Branch2"));

            var gamingConsoles = gamingConsoleRepository.All.ToList();

            gamingConsoles.Count().ShouldEqual(2);
            gamingConsoles.Any(console => console.BranchName == "Branch1" && console.Name == "Console1").ShouldBeTrue();
            gamingConsoles.Any(console => console.BranchName == "Branch2" && console.Name == "Console2").ShouldBeTrue();
        }

        private static GamingConsole CreateGamingConsole(string gameConsoleName, string branchName)
        {
            return new GamingConsole { Name = gameConsoleName, BranchName = branchName };
        }

        [Test]
        public void Gets_GamingConsole_by_guid()
        {
            var gamingConsole = CreateGamingConsole("foo", "bar");
            AddEntityToDatabase(gamingConsole);

            var actualGamingConsole = gamingConsoleRepository.GetById(gamingConsole.Id);

            actualGamingConsole.Id.ShouldEqual(gamingConsole.Id);
            actualGamingConsole.Name.ShouldEqual(gamingConsole.Name);
            actualGamingConsole.BranchName.ShouldEqual(gamingConsole.BranchName);
        }

        [Test]
        public void Deletes_GamingConsole_by_guid()
        {
            var gamingConsole = CreateGamingConsole("foo", "bar");
            AddEntityToDatabase(gamingConsole);

            gamingConsoleRepository.Delete(gamingConsole.Id);

            AssertDeletedEntity();
        }

        [Test]
        public void Updates_GamingConsole()
        {
            var gamingConsole = CreateGamingConsole("foo", "bar");
            AddEntityToDatabase(gamingConsole);
            gamingConsole.Name = "bar";

            gamingConsoleRepository.Update(gamingConsole);

            AssertUpdatedEntity(gamingConsole.Id, updatedGamingConsole =>
                                                    {
                                                        updatedGamingConsole.Name.ShouldEqual(gamingConsole.Name);
                                                        updatedGamingConsole.Id.ShouldEqual(gamingConsole.Id);
                                                        updatedGamingConsole.BranchName.ShouldEqual(gamingConsole.BranchName);
                                                    });
        }

        [Test]
        public void Checks_the_existance_of_GamingConsole_name_within_the_branch_with_case_ignored()
        {
            var gamingConsole1 = CreateGamingConsole("Console1", "Branch1");
            var gamingConsole2 = CreateGamingConsole("Console2", "Branch2");
            AddEntityToDatabase(gamingConsole1, gamingConsole2);

            var isGamingConsole1Exists = gamingConsoleRepository.IsConsoleNameExists("Console1".ToUpperInvariant(), "Branch1".ToUpperInvariant());
            var isGamingConsole2Exists = gamingConsoleRepository.IsConsoleNameExists("Console2".ToUpperInvariant(), "Branch2".ToUpperInvariant());

            isGamingConsole1Exists.ShouldBeTrue();
            isGamingConsole2Exists.ShouldBeTrue();
        }

        [Test]
        public void Checks_the_non_existance_of_GamingConsole_name_within_the_branch_with_case_ignored()
        {
            var gamingConsole1 = CreateGamingConsole("Console1", "Branch2");
            var gamingConsole2 = CreateGamingConsole("Console2", "Branch1");
            AddEntityToDatabase(gamingConsole1, gamingConsole2);

            var isGamingConsole1Exists = gamingConsoleRepository.IsConsoleNameExists("Console1".ToUpperInvariant(), "Branch1".ToUpperInvariant());
            var isGamingConsole2Exists = gamingConsoleRepository.IsConsoleNameExists("Console2".ToUpperInvariant(), "Branch2".ToUpperInvariant());

            isGamingConsole1Exists.ShouldBeFalse();
            isGamingConsole2Exists.ShouldBeFalse();
        }

        [Test]
        public void Retrieves_all_working_GamingConsoles_by_BranchName_with_case_ignored()
        {
            var gamingConsole1 = CreateGamingConsole("Console1", "Branch1");
            gamingConsole1.Status = Status.Working;
            var gamingConsole2 = CreateGamingConsole("Console2", "Branch1");
            gamingConsole2.Status = Status.UnderMaintenance;
            var gamingConsole3 = CreateGamingConsole("Console3", "Branch2");
            gamingConsole3.Status = Status.Unknown;
            var gamingConsole4 = CreateGamingConsole("Console4", "Branch2");
            gamingConsole4.Status = Status.Removed;
            var gamingConsole5 = CreateGamingConsole("Console5", "Branch2");
            gamingConsole5.Status = Status.Working;
            AddEntityToDatabase(gamingConsole1, gamingConsole2, gamingConsole3, gamingConsole4, gamingConsole5);


            var gamingConsolesInBranch1 = gamingConsoleRepository.GetGamingConsolesByBranchName("BrancH1").ToList();
            var gamingConsolesInBranch2 = gamingConsoleRepository.GetGamingConsolesByBranchName("BRANCH2").ToList();

            gamingConsolesInBranch1.ForEach(gamingConsole => gamingConsole.BranchName.ShouldEqual("Branch1"));
            gamingConsolesInBranch2.ForEach(gamingConsole => gamingConsole.BranchName.ShouldEqual("Branch2"));
            gamingConsolesInBranch1.Count.ShouldEqual(1);
            gamingConsolesInBranch2.Count.ShouldEqual(1);
            gamingConsolesInBranch1.First().Name.ShouldEqual("Console1");
            gamingConsolesInBranch2.First().Name.ShouldEqual("Console5");
        }
    }
}