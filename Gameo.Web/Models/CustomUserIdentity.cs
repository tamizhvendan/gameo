using System.Security.Principal;
using Gameo.Domain;

namespace Gameo.Web.Models
{
    public class CustomUserIdentity : IIdentity
    {
        private readonly User user;

        public CustomUserIdentity(User user)
        {
            this.user = user;
        }

        public string Name
        {
            get { return user.Name; }
        }

        public string AuthenticationType
        {
            get { return "custom"; }
        }

        public bool IsAuthenticated
        {
            get { return !string.IsNullOrEmpty(Name); }
        }

        public bool IsAdmin
        {
            get { return user.IsAdmin; }
        }
    }
}