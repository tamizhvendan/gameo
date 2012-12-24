using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Gameo.DataAccess.Core;
using Gameo.Domain;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using MongoDB.Driver.Linq;

namespace Gameo.DataAccess
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : Entity  
    {
        protected readonly MongoCollection<T> EntityCollection;

        protected RepositoryBase()
        {
            var mongoClient = new MongoClient();
            var mongoServer = mongoClient.GetServer();
            var gameoDatabase = mongoServer.GetDatabase(ConfigurationManager.AppSettings["database_name"]);
            EntityCollection = gameoDatabase.GetCollection<T>(typeof (T).Name.ToLowerInvariant());
        }

        public IEnumerable<T> All 
        {
            get { return EntityCollection.AsQueryable().ToList(); }
        }

        public void Add(T entity)
        {
            EntityCollection.Save(entity);
        }

        public void AddMany(IEnumerable<T> entities)
        {
            foreach (var entity in entities)
            {
                Add(entity);
            }
        }

        public T GetById(Guid guid)
        {
            return EntityCollection.AsQueryable().First(entity => entity.Id == guid);
        }

        public void Delete(Guid guid)
        {
            EntityCollection.Remove(Query<T>.EQ(entity => entity.Id, guid));
        }

        public void Update(T entityToBeUpdated)
        {
            EntityCollection.Save(entityToBeUpdated);
        }
    }
}