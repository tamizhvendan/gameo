using System;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using MongoDB.Driver.Linq;

namespace Gameo.DataAccess
{
    public class BranchRepository : RepositoryBase<Branch>, IBranchRepository
    {
        public bool IsBranchNameExists(string branchName)
        {
            return EntityCollection
                    .AsQueryable()
                    .Any(branch => branch.Name.ToLowerInvariant() == branchName.ToLowerInvariant());
        }
    }
}