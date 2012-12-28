using System;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Models;

namespace Gameo.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class MembershipController : ApplicationControllerBase
    {
        private readonly IMembershipRepository membershipRepository;

        public MembershipController(IMembershipRepository membershipRepository)
        {
            this.membershipRepository = membershipRepository;
        }

        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Create(CustomUserIdentity customUserIdentity)
        {
            return View(new Membership{ BranchName = customUserIdentity.BranchName});
        }

        [HttpPost]
        public ViewResult Create(Membership membership)
        {
            if (!ModelState.IsValid)
            {
                return View(membership);
            }

            membershipRepository.Add(membership);
            return View("MembershipCreated", membership);
        }
    }
}