using System;
using System.Collections.Generic;
using Gameo.Domain;

namespace Gameo.DataAccess.Core
{
    public interface IRepository<T> where T : Entity
    {
        IEnumerable<T> All { get; }
        void Add(T entity);
        void AddMany(IEnumerable<T> entities);
        T GetById(Guid guid);
        void Delete(Guid guid);
        void Update(T entityToBeUpdated);
    }
}