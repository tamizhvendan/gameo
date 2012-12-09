using System;
using System.Linq;
using Gameo.Domain;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class BranchRepositorySpec : RepositoryTestBase
    {
        private BranchRepository branchRepository;

        [SetUp]
        public void SetUp()
        {
            branchRepository = new BranchRepository();
        }

        [Test]
        public void Adds_new_branch()
        {
            var branchToAdd = new Branch {Name = "foo"};

            branchRepository.Add(branchToAdd);

            var branchCollection = GetCollection<Branch>();
            var actualBranchStored = branchCollection.AsQueryable().First();
            branchCollection.Count().ShouldEqual(1);
            actualBranchStored.Name.ShouldEqual(branchToAdd.Name);
            actualBranchStored.Id.ShouldNotEqual(Guid.Empty);
        }

        [Test]
        public void Lists_all_branches()
        {
            AddBranch(new Branch {Name = "Branch1"});
            AddBranch(new Branch { Name = "Branch2" });

            var availableBranches = branchRepository.All.ToList();

            availableBranches.Count().ShouldEqual(2);
            availableBranches.Any(branch => branch.Name == "Branch1").ShouldBeTrue();
            availableBranches.Any(branch => branch.Name == "Branch2").ShouldBeTrue();
        }

        private void AddBranch(Branch branch)
        {
            GetCollection<Branch>().Save(branch);
        }

        [Test]
        public void Gets_branch_by_guid()
        {
            var branch = new Branch {Name = "foo"};
            AddBranch(branch);

            var actualBranch = branchRepository.GetById(branch.Id);

            actualBranch.Id.ShouldEqual(branch.Id);
            actualBranch.Name.ShouldEqual(branch.Name);
        }

        [Test]
        public void Deletes_branch_by_guid()
        {
            var branch = new Branch { Name = "foo" };
            AddBranch(branch);

            branchRepository.Delete(branch.Id);

            GetCollection<Branch>().Count().ShouldEqual(0);
        }

        [Test]
        public void Updates_branch()
        {
            var branchToBeUpdated = new Branch { Name = "foo" };
            AddBranch(branchToBeUpdated);
            branchToBeUpdated.Name = "bar";

            branchRepository.Update(branchToBeUpdated);

            var actualUpdatedBranch = GetCollection<Branch>().Find(Query<Branch>.EQ(branch => branch.Id, branchToBeUpdated.Id)).First();
            actualUpdatedBranch.Name.ShouldEqual(branchToBeUpdated.Name);
            actualUpdatedBranch.Id.ShouldEqual(branchToBeUpdated.Id);
        }

        [Test]
        public void Checks_existence_of_branch_name_with_case_ignored()
        {
            var branch = new Branch { Name = "foo" };
            AddBranch(branch);

            var isBranchNameExists = branchRepository.IsBranchNameExists(branch.Name.ToUpperInvariant());

            isBranchNameExists.ShouldBeTrue();
        }

        [Test]
        public void Checks_non_existence_of_branch_name()
        {
            var branch = new Branch { Name = "foo" };
            AddBranch(branch);

            var isBranchNameExists = branchRepository.IsBranchNameExists("bar");

            isBranchNameExists.ShouldBeFalse();
        }
    }
}