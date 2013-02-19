using System;
using Gameo.DataAccess.Core;

namespace Gameo.Services
{
    public class GamingTrendService
    {
        private readonly IGameRepository gameRepository;
        private readonly IGamingConsoleRepository gamingConsoleRepository;

        public GamingTrendService(IGameRepository gameRepository, IGamingConsoleRepository gamingConsoleRepository)
        {
            this.gameRepository = gameRepository;
            this.gamingConsoleRepository = gamingConsoleRepository;
        }

        public void GetGamingTrendForAllConsoles(DateTime startDate, DateTime endDate, string branchName)
        {
            
        }
    }
}