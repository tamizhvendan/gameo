using System.Collections.Generic;
using System.Linq;
using Gameo.Domain;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    [TestFixture]
    public class GameRepositorySpec : RepositorySpecBase<Game>
    {
        [Test]
        public void Adds_many_game_entities()
        {
            var games = new List<Game>
                            {
                                new Game
                                    {
                                        ConsoleName = "Console1"
                                    },
                                new Game
                                    {
                                        ConsoleName = "Console2"
                                    }
                            };
            var gameRepository = new GameRepository();
            
            gameRepository.AddMany(games);

            AssertNewlyAddedManyEntities(entities =>
                                             {
                                                entities.Count().ShouldEqual(2);
                                                entities.First().ConsoleName.ShouldEqual("Console1");
                                                entities.Last().ConsoleName.ShouldEqual("Console2");
                                             });
        }
    }

    
}