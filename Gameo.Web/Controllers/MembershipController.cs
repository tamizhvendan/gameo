using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Models;
using Gameo.Web.ViewModels;

namespace Gameo.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class MembershipController : ApplicationControllerBase
    {
        private readonly IMembershipRepository membershipRepository;
        private readonly IGamingConsoleRepository gamingConsoleRepository;

        public MembershipController(IMembershipRepository membershipRepository, IGamingConsoleRepository gamingConsoleRepository)
        {
            this.membershipRepository = membershipRepository;
            this.gamingConsoleRepository = gamingConsoleRepository;
        }

        public ActionResult Index()
        {
            return RedirectToAction("MembershipDetail");
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

            if (membershipRepository.IsCustomer1ContactNumberExists(membership.Customer1ContactNumber))
            {
                ModelState.AddModelError("Customer1ContactNumber", "Customer 1 Contact Number already exists.");
                return View(membership);
            }

            membershipRepository.Add(membership);
            return View("MembershipCreated", membership);
        }

        public ViewResult MembershipDetail()
        {
            return View(new MembershipDetaiRequestViewModel());
        }

        [HttpPost]
        public ViewResult MembershipDetail(MembershipDetaiRequestViewModel membershipDetaiRequestViewModel)
        {
            if (!string.IsNullOrEmpty(membershipDetaiRequestViewModel.MembershipId))
            {
                membershipDetaiRequestViewModel.Membership =
                    membershipRepository.FindByMembershipId(membershipDetaiRequestViewModel.MembershipId);
            }

            if (!string.IsNullOrEmpty(membershipDetaiRequestViewModel.Customer1ContactNumber))
            {
                membershipDetaiRequestViewModel.Membership =
                    membershipRepository.FindByCustomer1ContactNumber(membershipDetaiRequestViewModel.Customer1ContactNumber);
            }
            return View(membershipDetaiRequestViewModel);
        }

        public ViewResult Recharge(CustomUserIdentity customUserIdentity)
        {
            var membershipReCharge = new MembershipReCharge {BranchName = customUserIdentity.BranchName};

            return View(membershipReCharge);
        }

        [HttpPost]
        public ActionResult Recharge(string membershipId, MembershipReCharge membershipReCharge)
        {
            if (!ModelState.IsValid)
            {
                return View(membershipReCharge);
            }

            if (membershipRepository.FindByMembershipId(membershipId) == null)
            {
                ModelState.AddModelError("membershipId", "Membership Id not exists.");
                return View(membershipReCharge);
            }

            membershipRepository.Recharge(membershipId, membershipReCharge);
            TempData["MembershipId"] = membershipId;

            return RedirectToAction("RechargeSuccess");
        }

        public ViewResult Membership()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Membership(string membershipId,CustomUserIdentity customUserIdentity)
        {
            var membership = membershipRepository.FindByMembershipId(membershipId);

            if (membership == null)
            {
                ModelState.AddModelError("membershipId", "Membership Id doesn't exists.");
                return View();
            }

            if (membership.IsExpired)
            {
                ModelState.AddModelError("membershipId", "Membership Id is expired. Kindly recharge.");
                return View();
            }

            RetrieveGamingConsolesAndPutItInViewBag(customUserIdentity.BranchName);

            return View("AssignConsole", new MembershipAssignConsoleViewModel{ Membership = membership });
        }

        [HttpPost]
        public ViewResult AssignConsole(MembershipAssignConsoleViewModel membershipAssignConsoleViewModel)
        {
            
            return View();
        }

        public ViewResult RechargeSuccess()
        {
            ViewBag.MembershipId = TempData["MembershipId"];
            return View();
        }

        private void RetrieveGamingConsolesAndPutItInViewBag(string branchName)
        {
            var gamingConsolesByBranchName = gamingConsoleRepository.GetGamingConsolesByBranchName(branchName);
            ViewBag.GamingConsoles =
                gamingConsolesByBranchName.Select(console => new SelectListItem { Text = console.Name, Value = console.Name });
        }
    }
}