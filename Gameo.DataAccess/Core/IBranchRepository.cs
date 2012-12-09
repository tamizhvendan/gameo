using Gameo.Domain;

namespace Gameo.DataAccess.Core
{
    public interface IBranchRepository : IRepository<Branch>
    {
        bool IsBranchNameExists(string branchName);
    }
}