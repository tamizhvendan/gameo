using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.MonthlyExpensesControllerSpecs
{
    [TestFixture]
    public class CreateGetActionSpec : MonthlyExpensesControllerSpecBase
    {
        [Test]
        public void Retrieves_list_of_branches_and_pass_it_to_view()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();

            var viewResult = MonthlyExpensesController.Create();

            AssertRandomBranchesPresentInViewBag(viewResult);
        }

        [Test]
        public void Returns_Create_view_With_MonthlyExpense_as_ViewModel()
        {
            var viewResult = MonthlyExpensesController.Create();

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldNotBeNull();
            viewResult.Model.ShouldBeType<MonthlyExpense>();
        }
    }
}