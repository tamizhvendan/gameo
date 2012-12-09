using Gameo.Domain;

namespace Gameo.DataAccess.Core
{
    public interface IGameConsoleRepository : IRepository<GameConsole>
    {
        bool IsConsoleNameExists(string consoleName, string branchName);
    }
}