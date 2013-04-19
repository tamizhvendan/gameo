using Gameo.Domain;

namespace Gameo.DataAccess.Core
{
    public interface IMonthlyExpensesRepository : IRepository<MonthlyExpense>
    {
        MonthlyExpense GetMonthlyExpenses(string branchName, int month, int year);
        bool HasExpenesRecoredForMonth(int month, int year, string branchName);
    }
}