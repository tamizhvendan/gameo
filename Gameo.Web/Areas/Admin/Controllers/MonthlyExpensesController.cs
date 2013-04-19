using System;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Controllers;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class MonthlyExpensesController : ApplicationControllerBase
    {
        private readonly IBranchRepository branchRepository;
        private readonly IMonthlyExpensesRepository monthlyExpensesRepository;

        public MonthlyExpensesController(IBranchRepository branchRepository, IMonthlyExpensesRepository monthlyExpensesRepository)
        {
            this.branchRepository = branchRepository;
            this.monthlyExpensesRepository = monthlyExpensesRepository;
        }

        public ViewResult Index()
        {
            return View(monthlyExpensesRepository.All.ToList());
        }

        public ViewResult Create()
        {
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);

            return View(new MonthlyExpense());
        }

        [HttpPost]
        public ActionResult Create(MonthlyExpense monthlyExpense)
        {
            if (monthlyExpensesRepository.HasExpenesRecoredForMonth(monthlyExpense.Month, monthlyExpense.Year, monthlyExpense.BranchName))
            {
                ModelState.AddModelError("Month", "Monthly Expenses already recorded for the given month.");
            }
            if (ModelState.IsValid)
            {
                monthlyExpensesRepository.Add(monthlyExpense);
                return RedirectToAction("Index");
            }

            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);

            return View(monthlyExpense);
        }
    }
}