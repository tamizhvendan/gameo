using System.Configuration;
using MongoDB.Driver;
using NUnit.Framework;

namespace Gameo.DataAccess.Tests
{
    public abstract class RepositoryTestBase
    {
        private readonly MongoDatabase gameoTestDatabase;

        protected RepositoryTestBase()
        {
            var mongoClient = new MongoClient();
            var mongoServer = mongoClient.GetServer();
            gameoTestDatabase = mongoServer.GetDatabase(ConfigurationManager.AppSettings["database_name"]);
        }

        protected MongoCollection<T> GetCollection<T>() where T : class
        {
            return gameoTestDatabase.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        [TearDown]
        protected void DropTestDatabase()
        {
            gameoTestDatabase.Drop();
        }
    }
}