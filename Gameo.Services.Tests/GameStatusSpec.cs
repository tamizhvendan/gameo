using System.Linq;
using NUnit.Framework;
using Should;

namespace Gameo.Services.Tests
{
    [TestFixture]
    public class GameStatusSpec
    {
        [Test]
        public void Has_GamingConsoleName()
        {
            var gameStatus = new GameStatus("Console1");
            gameStatus.GamingConsoleName.ShouldEqual("Console1");
        }

        [Test]
        public void Adds_Players()
        {
            var gameStatus = new GameStatus("foo");
            var player = new Player();
            
            gameStatus.AddPlayer(player);

            gameStatus.Players.Count().ShouldEqual(1);
            gameStatus.Players.First().ShouldEqual(player);
        }
    }
}