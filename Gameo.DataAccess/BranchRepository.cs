using System.Configuration;
using Gameo.Domain;
using MongoDB.Driver;

namespace Gameo.DataAccess
{
    public class BranchRepository
    {
        private readonly MongoDatabase gameoDatabase;

        public BranchRepository()
        {
            var mongoClient = new MongoClient();
            var mongoServer = mongoClient.GetServer();
            gameoDatabase = mongoServer.GetDatabase(ConfigurationManager.AppSettings["database_name"]);
        }

        public void Add(Branch branch)
        {
            var mongoCollection = gameoDatabase.GetCollection<Branch>(typeof (Branch).Name.ToLowerInvariant());
            mongoCollection.Save(branch);
        }
    }
}