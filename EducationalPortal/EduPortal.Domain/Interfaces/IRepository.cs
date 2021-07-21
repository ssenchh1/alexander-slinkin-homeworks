using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EduPortal.Domain.Interfaces
{
    public interface IRepository<T>
    {
        Task AddAsync(T obj);

        Task UpdateAsync(T obj);

        Task DeleteAsync(T obj);

        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync(int id, string includeProperties);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int skip = 0, int take = 0);

        Task<PagedList<T>> GetPagedAsync(int pageNumber = 0, int pageSize = 0, Expression<Func<T, bool>> filter = null, string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);
    }
}
