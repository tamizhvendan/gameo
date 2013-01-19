using System;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Controllers;

namespace Gameo.Web.Areas.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserController : ApplicationControllerBase
    {
        private readonly IUserRepository userRepository;
        private readonly IBranchRepository branchRepository;

        public UserController(IUserRepository userRepository, IBranchRepository branchRepository)
        {
            this.userRepository = userRepository;
            this.branchRepository = branchRepository;
        }

        public ViewResult Create()
        {
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);
            return View(new User());
        }

        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                if (!userRepository.IsUserNameExists(user.Name, user.BranchName))
                {
                    userRepository.Add(user);
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError("name", "User name already exists in the selected branch");
            }
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);
            return View(user);
        }

        public ViewResult Index()
        {
            var users = userRepository.All;
            return View(users);
        }

        public RedirectToRouteResult DeActivate(Guid id)
        {
            userRepository.DeActivateUser(id);

            return RedirectToAction("Index");
        }

        public RedirectToRouteResult Activate(Guid id)
        {
            userRepository.ActivateUser(id);

            return RedirectToAction("Index");
        }
    }
}