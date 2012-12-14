﻿using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;

namespace Gameo.Web.Areas.Admin.Controllers
{
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
                ModelState.AddModelError("Name", "User Name already exists in the selected branch");
            }
            ViewBag.Branches = MapBranchesToSelectListItems(branchRepository);
            return View(user);
        }

        public ViewResult Index()
        {
            var users = userRepository.All;
            return View(users);
        }
    }
}