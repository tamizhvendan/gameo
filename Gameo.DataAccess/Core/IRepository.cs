using System.Collections.Generic;

namespace Gameo.DataAccess.Core
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> All { get; }
        void Add(T entity);
    }
}