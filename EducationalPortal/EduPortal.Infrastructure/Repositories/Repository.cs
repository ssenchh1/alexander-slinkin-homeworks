using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EduPortal.Domain;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models;
using EduPortal.Infrastructure.Context;
using EduPortal.Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly EducationalPortalContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(EducationalPortalContext dbcontext)
        {
            _context = dbcontext;
            _dbSet = _context.Set<T>();
        }

        public async Task AddAsync(T obj)
        {
            await _dbSet.AddAsync(obj);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T obj)
        {
            _dbSet.Update(obj);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T obj)
        {
            _dbSet.Remove(obj);
            await _context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<T> GetByIdAsync(int id, string includeProperties)
        {
            var result = _dbSet.Where(c => c.Id == id);

            foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(property);
            }

            return await result.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter = null, string includeProperties = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int skip = 0, int take = 0)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip != 0 && skip > 0)
            {
                query = query.Skip(skip);
            }

            if (take != 0 && take > 0)
            {
                query = query.Take(take);
            }

            return await query.ToListAsync();
        }

        public async Task<PagedList<T>> GetPagedAsync(int pageNumber = 0, int pageSize = 0, Expression<Func<T, bool>> filter = null, string includeProperties = "", Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToPagedAsync<T>(pageNumber, pageSize);
        }
    }
}
