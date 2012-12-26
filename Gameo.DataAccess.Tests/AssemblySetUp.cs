using NUnit.Framework;

namespace Gameo.DataAccess.Tests
{
    [SetUpFixture]
    public class AssemblySetUp
    {
        [SetUp]
        public void AssemblySpecInit()
        {
            MongoDbMapping.Initialize();
        }
    }
}