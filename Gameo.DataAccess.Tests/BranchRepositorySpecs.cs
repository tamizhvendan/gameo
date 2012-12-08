using System.Linq;
using Gameo.Domain;
using MongoDB.Driver.Linq;
using NUnit.Framework;
using Should;
using Should.Core;
using Should.Fluent;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class BranchRepositorySpecs : RepositoryTestBase
    {
        [Test]
        public void Adds_new_branch()
        {
            var branchRepository = new BranchRepository();
            var branchToAdd = new Branch {Name = "foo"};

            branchRepository.Add(branchToAdd);

            var branchCollection = GetMongoCollection<Branch>();
            var actualBranchStored = branchCollection.AsQueryable().First();
            branchCollection.Count().ShouldEqual(1);
            actualBranchStored.Name.ShouldEqual(branchToAdd.Name);
            actualBranchStored.Id.Should().Not.Be.Null();
        }
    }
}