using System;
using Gameo.Domain;

namespace Gameo.DataAccess.Core
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserNameExists(string userName, string branchName);
        void DeActivateUser(Guid id);
        void ActivateUser(Guid id);
    }
}