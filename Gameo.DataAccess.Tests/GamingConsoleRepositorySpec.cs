﻿using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class GamingConsoleRepositorySpec : RepositoryTestBase<GamingConsole>
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
    }
}