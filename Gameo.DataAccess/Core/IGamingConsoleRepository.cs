using Gameo.Domain;

namespace Gameo.DataAccess.Core
{
    public interface IGamingConsoleRepository : IRepository<GamingConsole>
    {
        bool IsConsoleNameExists(string consoleName, string branchName);
    }
}