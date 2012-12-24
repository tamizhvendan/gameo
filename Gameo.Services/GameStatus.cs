using System.Collections.Generic;

namespace Gameo.Services
{
    public class GameStatus
    {
        public string GamingConsoleName { get; private set; }

        private readonly List<Player> players;

        public IEnumerable<Player> Players
        {
            get { return players; }
        }

        public GameStatus(string gameConsoleName)
        {
            GamingConsoleName = gameConsoleName;
            players = new List<Player>();
        }

        public void AddPlayer(Player player)
        {
            players.Add(player);
        }
    }
}