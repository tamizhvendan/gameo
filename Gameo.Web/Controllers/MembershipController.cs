using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Services;
using Gameo.Web.Models;
using Gameo.Web.ViewModels;

namespace Gameo.Web.Controllers
{
    [Authorize(Roles = "user")]
    public class MembershipController : ApplicationControllerBase
    {
        private readonly IMembershipRepository membershipRepository;
        private readonly IGamingConsoleRepository gamingConsoleRepository;
        private readonly IGameService gameService;

        public MembershipController(IMembershipRepository membershipRepository, IGamingConsoleRepository gamingConsoleRepository, IGameService gameService)
        {
            this.membershipRepository = membershipRepository;
            this.gamingConsoleRepository = gamingConsoleRepository;
            this.gameService = gameService;
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
        public ActionResult Recharge(MembershipReCharge membershipReCharge)
        {
            if (!ModelState.IsValid)
            {
                return View(membershipReCharge);
            }

            if (membershipRepository.FindByMembershipId(membershipReCharge.MembershipId) == null)
            {
                ModelState.AddModelError("MembershipId", "Membership Id not exists.");
                return View(membershipReCharge);
            }

            membershipRepository.Recharge(membershipReCharge);
            TempData["MembershipId"] = membershipReCharge.MembershipId;

            return RedirectToAction("RechargeSuccess");
        }

        public ViewResult Membership()
        {
            return View();
        }

        [HttpPost]
        public ViewResult Membership(string membershipId, CustomUserIdentity customUserIdentity)
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

            return View("AssignConsole", 
                new MembershipAssignConsoleViewModel
                    {
                        Membership = membership, Game = new Game { GamePaymentType = GamePaymentType.Membership, BranchName = customUserIdentity.BranchName}
                    });
        }

        [HttpPost]
        public ActionResult AssignConsole(MembershipAssignConsoleViewModel membershipAssignConsoleViewModel)
        {
            if (!ModelState.IsValid)
            {
                RetrieveGamingConsolesAndPutItInViewBag(membershipAssignConsoleViewModel.Game.BranchName);
                return View(membershipAssignConsoleViewModel);
            }

            var membership = membershipRepository.FindByMembershipId(membershipAssignConsoleViewModel.Membership.MembershipId);

            if (membershipAssignConsoleViewModel.Game.HoursPlayed > membership.RemainingHours)
            {
                RetrieveGamingConsolesAndPutItInViewBag(membershipAssignConsoleViewModel.Game.BranchName);
                ModelState.AddModelError("Game", string.Format("Membership has only {0} hours. Please recharge!", membership.RemainingHours));
                return View(membershipAssignConsoleViewModel);
            }

            gameService.AssignConsoleForMembership(membership, membershipAssignConsoleViewModel.Game);
            TempData["Message"] = "Game assigned to membership successfully";
            return RedirectToAction("Index", "Game");
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