using NUnit.Framework;
using Should;

namespace Gameo.Domain.Tests
{
    [TestFixture]
    public class MonthlyExpensesSpec : EntitySpecBase
    {
        private MonthlyExpense monthlyExpense;

        [SetUp]
        public void SetUp()
        {
            monthlyExpense = new MonthlyExpense {SalaryPaid = 10, BranchName = "foo"};
        }

        [Test]
        [TestCase(0, false)]
        [TestCase(-0.5, false)]
        [TestCase(1.5, true)]
        public void SalaryPaidShouldBeMoreThanZero(decimal salaryPaid, bool isHappyPath)
        {
            monthlyExpense.SalaryPaid = salaryPaid;
            if (isHappyPath)
            {
                AssertZeroValidationError(monthlyExpense);
            }
            else
            {
                AssertEntityValidationError(monthlyExpense, "Salary Paid should be more than zero.");
            }
        }

        [Test]
        [TestCase("", false)]
        [TestCase(null, false)]
        [TestCase("foo", true)]
        public void BranchName_is_required(string branchName, bool isHappyPath)
        {
            monthlyExpense.BranchName = branchName;
            if (isHappyPath)
            {
                AssertZeroValidationError(monthlyExpense);
            }
            else
            {
                AssertEntityValidationError(monthlyExpense, "Branch Name is required.");    
            }
        }

        [Test]
        public void TotalExpenses_is_the_sum_of_all_expenses()
        {
            monthlyExpense.EbPayment = 1;
            monthlyExpense.InternetBill = 2;
            monthlyExpense.OtherExpenses = 3;
            monthlyExpense.Rent = 4;
            monthlyExpense.SalaryPaid = 5;

            monthlyExpense.TotalExpenses.ShouldEqual(15);
        }
    }
}