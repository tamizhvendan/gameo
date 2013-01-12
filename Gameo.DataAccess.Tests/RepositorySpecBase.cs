using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Gameo.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;
using NUnit.Framework;
using Should;

namespace Gameo.DataAccess.Tests
{
    public abstract class RepositorySpecBase<T> where T : Entity
    {
        private readonly MongoDatabase gameoTestDatabase;

        private MongoCollection<T> collection;

        protected RepositorySpecBase()
        {
            var mongoUrl = new MongoUrl(ConfigurationManager.AppSettings["database_connection_string"]);
            var mongoClientSettings = MongoClientSettings.FromUrl(mongoUrl);
            var mongoClient = new MongoClient(mongoClientSettings);
            var mongoServer = mongoClient.GetServer();
            gameoTestDatabase = mongoServer.GetDatabase(mongoUrl.DatabaseName);
        }

        [SetUp]
        public void InitDatabase()
        {
            collection = gameoTestDatabase.GetCollection<T>(typeof(T).Name.ToLowerInvariant());
        }

        protected void AssertNewlyAddedEntity(Action<T> assertEntityDelegate)
        {
            var actualEntitySaved = collection.AsQueryable().First();
            collection.Count().ShouldEqual(1);
            actualEntitySaved.Id.ShouldNotEqual(Guid.Empty);
            assertEntityDelegate(actualEntitySaved);
        }

        protected void AssertNewlyAddedManyEntities(Action<IEnumerable<T>> assertEntitiesDelegate)
        {
            assertEntitiesDelegate(collection.FindAll());
        }

        protected void AssertDeletedEntity()
        {
            collection.Count().ShouldEqual(0);
        }

        protected void AssertUpdatedEntity(Guid id, Action<T> assertEntityDelegate)
        {
            var actualUpdatedEntity = collection.Find(Query<T>.EQ(entity => entity.Id, id)).First();
            assertEntityDelegate(actualUpdatedEntity);
        }

        protected void AddEntityToDatabase(T entity)
        {
            collection.Save(entity);
        }

        protected void AddEntityToDatabase(params T[] entities)
        {
            foreach (var entity in entities)
            {
                AddEntityToDatabase(entity);
            }
        }

        [TearDown]
        protected void TearDown()
        {
            collection.Drop();
        }
    }
}