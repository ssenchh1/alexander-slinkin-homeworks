using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models.Users;
using EduPortal.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Infrastructure.Repositories
{
    public class UserRepository : IGenericUserRepository<User>
    {
        private EducationalPortalContext db;

        public UserRepository(EducationalPortalContext dbcontext)
        {
            db = dbcontext;
        }

        public async Task AddAsync(User obj)
        {
            await db.Users.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(User obj)
        {
            db.Users.Update(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(User obj)
        {
            db.Remove(obj);
            await db.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await db.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> Get(Expression<Func<User, bool>> filter = null)
        {
            if (filter != null)
            {
                return await db.Users.Where(filter).ToListAsync();
            }
            else
            {
                return await db.Users.ToListAsync();
            }
        }

        public async Task<User> GetByIdAsync(string id, string includeProperties)
        {
            var result = db.Users.Where(c => c.Id == id);

            foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(property);
            }

            return await result.FirstOrDefaultAsync();
        }
    }
}
