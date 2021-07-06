using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Practice9june.Core.Interfaces
{
    public interface IRepository<T>
    {
        void Add(T obj);
        void Delete(T obj);

        IEnumerable<T> Get(Expression<Func<T, bool>> filter = null);
    }
}
