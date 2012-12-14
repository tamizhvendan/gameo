using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Gameo.DataAccess.Core;
using Gameo.Domain;

namespace Gameo.Web.Areas.Admin.Controllers
{
    public class ApplicationControllerBase : Controller
    {
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