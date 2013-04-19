using Gameo.DataAccess.Core;
using Gameo.Web.Areas.Admin.Controllers;
using Moq;
using NUnit.Framework;

namespace Gameo.Web.Tests.AdminSpecs.MonthlyExpensesControllerSpecs
{
    public class MonthlyExpensesControllerSpecBase : ControllerSpecBase
    {
        protected Mock<IMonthlyExpensesRepository> MonthlyExpensesRepositoryMock;
        protected MonthlyExpensesController MonthlyExpensesController;

        [SetUp]
        public void MonthlyExpensesControllerSpecSetUp()
        {
            MonthlyExpensesRepositoryMock = new Mock<IMonthlyExpensesRepository>();
            MonthlyExpensesController = new MonthlyExpensesController(BranchRepositoryMock.Object, MonthlyExpensesRepositoryMock.Object);
        }
    }
}