using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;

namespace Gameo.Web.Areas.Admin.Controllers
{
    public class BranchController : Controller
    {
        private readonly IBranchRepository branchRepository;

        public BranchController(IBranchRepository branchRepository)
        {
            this.branchRepository = branchRepository;
        }

        public ViewResult Index()
        {
            return View(branchRepository.All);
        }

        public ViewResult Create()
        {
            return View(new Branch());
        }

        [HttpPost]
        public ActionResult Create(Branch branch)
        {
            if (ModelState.IsValid)
            {
                if (!branchRepository.IsBranchNameExists(branch.Name))
                {
                    branchRepository.Add(branch);
                    return RedirectToAction("Index");        
                }
                ModelState.AddModelError("Name", "Branch Name already exists");
            }
            return View(branch);
        }
    }
}