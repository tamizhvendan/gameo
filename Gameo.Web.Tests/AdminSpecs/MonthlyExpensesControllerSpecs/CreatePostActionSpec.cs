using System.Web.Mvc;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.Web.Tests.AdminSpecs.MonthlyExpensesControllerSpecs
{
    [TestFixture]
    public class CreatePostActionSpec : MonthlyExpensesControllerSpecBase
    {
        private MonthlyExpense monthlyExpense;

        [SetUp]
        public void SetUp()
        {
            monthlyExpense = new MonthlyExpense { BranchName = "foo", Year = 2012, Month = 1};
        }

        [Test]
        public void Returns_Create_View_With_Model_errors_if_invalid_MonthlyExpense_posted()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();
            MonthlyExpensesController.ModelState.AddModelError("SalaryPaid", "Salary Paid should be more than zero.");

            var viewResult = MonthlyExpensesController.Create(monthlyExpense) as ViewResult;
            
            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertModelError(MonthlyExpensesController, "SalaryPaid", "Salary Paid should be more than zero.");
            AssertRandomBranchesPresentInViewBag(viewResult);
        }

        [Test]
        public void Returns_Create_View_model_error_if_monthly_expense_already_added_for_given_month()
        {
            SetupBranchRepositoryToReturnSomeRandomBranches();
            MonthlyExpensesRepositoryMock
                .Setup(repo => repo.HasExpenesRecoredForMonth(monthlyExpense.Month, monthlyExpense.Year, monthlyExpense.BranchName))
                .Returns(true);

            var viewResult = MonthlyExpensesController.Create(monthlyExpense) as ViewResult;

            viewResult.ViewName.ShouldEqual(string.Empty);
            AssertModelError(MonthlyExpensesController, "Month", "Monthly Expenses already recorded for the given month.");
            AssertRandomBranchesPresentInViewBag(viewResult);
        }

        [Test]
        public void Add_to_the_repository_when_monthly_expense_posted_is_valid()
        {
            MonthlyExpensesRepositoryMock.Setup(repo => repo.Add(monthlyExpense)).Verifiable();

            MonthlyExpensesController.Create(monthlyExpense);

            MonthlyExpensesRepositoryMock.Verify(repo => repo.Add(monthlyExpense));
        }

        [Test]
        public void Redirect_to_index_view_upon_successful_creation()
        {
            var actionResult = MonthlyExpensesController.Create(monthlyExpense);

            AssertReadirectToAction(actionResult, "Index");
        }
    }
}