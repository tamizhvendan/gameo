using System;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using MongoDB.Driver.Linq;

namespace Gameo.DataAccess
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public bool IsUserNameExists(string userName, string branchName)
        {
            return EntityCollection
                    .AsQueryable()
                    .Any(user => user.Name.ToLowerInvariant() == userName.ToLowerInvariant() 
                                    && user.BranchName.ToLowerInvariant() == branchName.ToLowerInvariant());
        }

        public void DeActivateUser(Guid id)
        {
            UpdateUserStatus(id, false);
        }

        public void ActivateUser(Guid id)
        {
            UpdateUserStatus(id, true);
        }

        private void UpdateUserStatus(Guid id, bool isActive)
        {
            var user = GetById(id);
            user.IsActive = isActive;
            Update(user);
        }
    }
}