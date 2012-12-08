using System.Collections.Generic;
using System.Web.Mvc;

namespace Gameo.Web.ViewModels
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public int BranchId { get; set; }
        public IEnumerable<SelectListItem> BranchSelectListItems { get; set; }
    }
}