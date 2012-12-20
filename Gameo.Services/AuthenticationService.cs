using System;
using System.Web.Security;
using Gameo.DataAccess.Core;
using Gameo.Domain;

namespace Gameo.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserRepository userRepository;

        public AuthenticationService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public User Authenticate(string userName, string password, string branchName)
        {
            var user = userRepository.GetByUserName(userName);
            if (user.Password == password)
            {
                if (user.IsAdmin)
                    return user;
                if (user.BranchName == branchName)
                    return user;
            }
            throw new ArgumentException("Invalid User Credentials");
        }

        public void SetAuthCookie(string userName)
        {
            FormsAuthentication.SetAuthCookie(userName, false);
        }
    }
}