using System;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using MongoDB.Driver.Linq;

namespace Gameo.DataAccess
{
    public class MonthlyExpensesRepository : RepositoryBase<MonthlyExpense>, IMonthlyExpensesRepository
    {
        public MonthlyExpense GetMonthlyExpenses(string branchName, int month, int year)
        {
            return EntityCollection
                .AsQueryable()
                .FirstOrDefault(expense => expense.BranchName == branchName && expense.Month == month && expense.Year == year );
        }

        public bool HasExpenesRecoredForMonth(int month, int year, string branchName)
        {
            return GetMonthlyExpenses(branchName, month, year) != null;
        }
    }
}