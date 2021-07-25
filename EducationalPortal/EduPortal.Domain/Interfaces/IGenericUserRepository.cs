using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EduPortal.Domain.Models.Users;

namespace EduPortal.Domain.Interfaces
{
    public interface IGenericUserRepository<T> where T : User
    {
        Task AddAsync(T obj);

        Task UpdateAsync(T obj);

        Task DeleteAsync(T obj);

        Task<T> GetByIdAsync(string id);

        Task<T> GetByIdAsync(string id, string includeProperties);

        Task<IEnumerable<T>> Get(Expression<Func<T, bool>> filter = null);
    }
}
