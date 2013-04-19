using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.MonthlyExpensesControllerSpecs
{
    [TestFixture]
    public class IndexGetActionSpec : MonthlyExpensesControllerSpecBase
    {
        [Test]
        public void Rerieves_all_monthly_expenses_and_pass_it_to_index_view()
        {
            var monthlyExpenses= new[] {new MonthlyExpense {BranchName = "foo"}};
            MonthlyExpensesRepositoryMock.Setup(repo => repo.All).Returns(monthlyExpenses);

            var viewResult = MonthlyExpensesController.Index();

            viewResult.ViewName.ShouldEqual(string.Empty);
            viewResult.Model.ShouldEqual(monthlyExpenses);
        }
    }
}