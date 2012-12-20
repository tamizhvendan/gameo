using System;
using System.Security.Principal;

namespace Gameo.Web.Models
{
    public class CustomUserPrinciple : IPrincipal
    {
        private readonly CustomUserIdentity userIdentity;

        public CustomUserPrinciple(CustomUserIdentity userIdentity)
        {
            this.userIdentity = userIdentity;
        }

        public bool IsInRole(string role)
        {
            if (role.Equals("admin", StringComparison.InvariantCultureIgnoreCase) && userIdentity.IsAdmin)
                return true;
            if (role.Equals("user", StringComparison.InvariantCultureIgnoreCase) && !userIdentity.IsAdmin)
                return true;
            return false;
        }

        public IIdentity Identity
        {
            get { return userIdentity; }
        }
    }
}