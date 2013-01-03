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

        public MembershipController(IMembershipRepository membershipRepository)
        {
            this.membershipRepository = membershipRepository;
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
        public ViewResult Recharge(string membershipId, MembershipReCharge membershipReCharge)
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

            ViewBag.MembershipId = membershipId;
            return View("RechargeSuccess", membershipReCharge);

        }
    }
}