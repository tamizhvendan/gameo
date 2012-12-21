using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using Gameo.Web.Models;

namespace Gameo.Web.Controllers
{
    public class ApplicationControllerBase : Controller
    {
        protected override void OnAuthorization(AuthorizationContext filterContext)
        {
            var loggedUser = Session["logged_user"] as User;
            if (loggedUser != null)
            {
                filterContext.HttpContext.User = new CustomUserPrinciple(new CustomUserIdentity(loggedUser));
            }
            base.OnAuthorization(filterContext);
        }
        
        protected IEnumerable<SelectListItem> MapBranchesToSelectListItems(IBranchRepository branchRepository)
        {
            return branchRepository
                    .All
                    .Select(branch => new SelectListItem
                    {
                        Text = branch.Name,
                        Value = branch.Name
                    });
        }
    }
}