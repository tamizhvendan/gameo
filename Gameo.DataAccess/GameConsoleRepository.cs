using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using MongoDB.Driver.Linq;

namespace Gameo.DataAccess
{
    public class GameConsoleRepository : RepositoryBase<GameConsole>, IGameConsoleRepository
    {
        public bool IsConsoleNameExists(string consoleName, string branchName)
        {
            return EntityCollection
                .AsQueryable()
                .Any(gameConsole => gameConsole.Name.ToLowerInvariant() == consoleName.ToLowerInvariant() &&
                                    gameConsole.BranchName.ToLowerInvariant() == branchName.ToLowerInvariant());
        }
    }
}