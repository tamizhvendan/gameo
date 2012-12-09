using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class BranchRepositorySpec : RepositoryTestBase<Branch>
    {
        private BranchRepository branchRepository;

        [SetUp]
        public void SetUp()
        {
            branchRepository = new BranchRepository();
        }

        private Branch CreateBranch(string branchName)
        {
            return new Branch {Name = branchName};
        }

        [Test]
        public void Adds_new_branch()
        {
            var branchToAdd = CreateBranch("foo");

            branchRepository.Add(branchToAdd);

            AssertNewlyAddedEntity(actualEntityStored => actualEntityStored.Name.ShouldEqual(branchToAdd.Name));
        }

        [Test]
        public void Lists_all_branches()
        {
            AddEntityToDatabase(CreateBranch("foo"));
            AddEntityToDatabase(CreateBranch("bar"));

            var availableBranches = branchRepository.All.ToList();

            availableBranches.Count().ShouldEqual(2);
            availableBranches.Any(branch => branch.Name == "foo").ShouldBeTrue();
            availableBranches.Any(branch => branch.Name == "bar").ShouldBeTrue();
        }

        [Test]
        public void Gets_branch_by_guid()
        {
            var branch = CreateBranch("foo");
            AddEntityToDatabase(branch);

            var actualBranch = branchRepository.GetById(branch.Id);

            actualBranch.Id.ShouldEqual(branch.Id);
            actualBranch.Name.ShouldEqual(branch.Name);
        }

        [Test]
        public void Deletes_branch_by_guid()
        {
            var branch = CreateBranch("foo");
            AddEntityToDatabase(branch);

            branchRepository.Delete(branch.Id);

            collection.Count().ShouldEqual(0);
        }

        [Test]
        public void Updates_branch()
        {
            var branchToBeUpdated = CreateBranch("foo");
            AddEntityToDatabase(branchToBeUpdated);
            branchToBeUpdated.Name = "bar";

            branchRepository.Update(branchToBeUpdated);

            AssertUpdatedEntity(branchToBeUpdated.Id, actualUpdatedBranch =>
                                                          {
                                                              actualUpdatedBranch.Name.ShouldEqual(branchToBeUpdated.Name);
                                                              actualUpdatedBranch.Id.ShouldEqual(branchToBeUpdated.Id);
                                                          });
        }

        [Test]
        public void Checks_existence_of_branch_name_with_case_ignored()
        {
            var branch = CreateBranch("foo");
            AddEntityToDatabase(branch);

            var isBranchNameExists = branchRepository.IsBranchNameExists(branch.Name.ToUpperInvariant());

            isBranchNameExists.ShouldBeTrue();
        }

        [Test]
        public void Checks_non_existence_of_branch_name()
        {
            var branch = CreateBranch("foo");
            AddEntityToDatabase(branch);

            var isBranchNameExists = branchRepository.IsBranchNameExists("bar");

            isBranchNameExists.ShouldBeFalse();
        }
    }
}