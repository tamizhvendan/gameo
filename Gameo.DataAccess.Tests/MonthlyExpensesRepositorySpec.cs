using System;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class MonthlyExpensesRepositorySpec : RepositorySpecBase<MonthlyExpense>
    {
        private MonthlyExpensesRepository monthlyExpensesRepository;

        [SetUp]
        public void SetUp()
        {
            monthlyExpensesRepository = new MonthlyExpensesRepository();
        }

        [Test]
        public void Get_the_monthly_expenses_for_given_branch_on_given_month_and_year()
        {
            var jan2011OnBranch1 = new MonthlyExpense {Month = 1, Year = 2011, EbPayment = 1500, Rent = 2500, BranchName = "branch1"};
            var march2013OnBranch1 = new MonthlyExpense {Month = 3, Year = 2013, EbPayment = 1000, Rent = 3500, BranchName = "branch1"};
            var feb2012OnBranch2 = new MonthlyExpense {Month = 2, Year = 2012, EbPayment = 1000, Rent = 4000, BranchName = "branch2"};
            AddEntityToDatabase(jan2011OnBranch1);
            AddEntityToDatabase(march2013OnBranch1);
            AddEntityToDatabase(feb2012OnBranch2);

            var monthlyExpenses = monthlyExpensesRepository.GetMonthlyExpenses("branch1", 3, 2013);

            monthlyExpenses.TotalExpenses.ShouldEqual(4500);
        }

        [Test]
        public void Return_null_if_monthly_expenses_not_available_for_given_date()
        {
            var monthlyExpenses = monthlyExpensesRepository.GetMonthlyExpenses("branch1", 4, 2011);

            monthlyExpenses.ShouldBeNull();
        }

        [Test]
        public void Can_add_monthly_expenses()
        {
            var jan2011OnBranch1 = new MonthlyExpense { Month = 1, Year = 2011, EbPayment = 1500, Rent = 2500, BranchName = "branch1" };

            monthlyExpensesRepository.Add(jan2011OnBranch1);

            AssertNewlyAddedEntity(addedExpense =>
                {
                    addedExpense.BranchName.ShouldEqual("branch1");
                    addedExpense.TotalExpenses.ShouldEqual(4000);
                });
        }

        [Test]
        [TestCase(1, 2011, "branch1", true)]
        [TestCase(3, 2013, "branch1", true)]
        [TestCase(2, 2012, "branch2", true)]
        [TestCase(1, 2012, "branch2", false)]
        [TestCase(1, 2012, "branch1", false)]
        public void Can_Verify_whether_monthly_expenses_Recorded_for_given_date(int month, int year, string branchName, bool expectedResult)
        {
            var jan2011OnBranch1 = new MonthlyExpense { Month = 1, Year = 2011, EbPayment = 1500, Rent = 2500, BranchName = "branch1" };
            var march2013OnBranch1 = new MonthlyExpense { Month = 3, Year = 2013, EbPayment = 1000, Rent = 3500, BranchName = "branch1" };
            var feb2012OnBranch2 = new MonthlyExpense { Month = 2, Year = 2012, EbPayment = 1000, Rent = 4000, BranchName = "branch2" };
            AddEntityToDatabase(jan2011OnBranch1);
            AddEntityToDatabase(march2013OnBranch1);
            AddEntityToDatabase(feb2012OnBranch2);

            var hasExpenesRecoredForMonth = monthlyExpensesRepository.HasExpenesRecoredForMonth(month, year, branchName);

            hasExpenesRecoredForMonth.ShouldEqual(expectedResult);
        }
    }
}