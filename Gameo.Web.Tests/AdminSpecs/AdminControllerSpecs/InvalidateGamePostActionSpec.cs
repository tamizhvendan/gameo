using System;
using NUnit.Framework;

namespace Gameo.Web.Tests.AdminSpecs.AdminControllerSpecs
{
    [TestFixture]
    public class InvalidateGamePostActionSpec : AdminControllerSpecBase
    {
        [Test]
        public void Invalidate_game_using_game_service()
        {
            var id = Guid.Empty;
            GameServiceMock.Setup(service => service.MarkGameAsInvalid(id)).Verifiable();

            AdminController.InvalidateGame(id);

            GameServiceMock.Verify(service => service.MarkGameAsInvalid(id));
        }
    }
}