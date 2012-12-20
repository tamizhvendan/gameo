using Gameo.Domain;

namespace Gameo.Services
{
    public interface IAuthenticationService
    {
        User Authenticate(string userName, string password, string branchName);
        void SetAuthCookie(string userName);
    }
}