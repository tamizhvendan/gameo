using System.Web.Mvc;

namespace Gameo.Web.Models
{
    public class CustomUserIdentityModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            return controllerContext.HttpContext.User.Identity as CustomUserIdentity;
        }
    }
}