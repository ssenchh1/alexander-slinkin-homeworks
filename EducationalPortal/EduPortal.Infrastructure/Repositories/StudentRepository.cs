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
    public class StudentRepository : IUserRepository<Student>
    {
        private EducationalPortalContext db;

        public StudentRepository(EducationalPortalContext dbcontext)
        {
            db = dbcontext;
        }

        public async Task AddAsync(Student obj)
        {
            await db.Students.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Student obj)
        {
            db.Students.Update(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student obj)
        {
            db.Students.Remove(obj);
            await db.SaveChangesAsync();
        }

        public async Task<Student> GetByIdAsync(string id)
        {
            return await db.Students.FirstAsync(s => s.Id == id);
        }

        public async Task<Student> GetByIdAsync(string id, string includeProperties)
        {
            var result = db.Students.Where(c => c.Id == id);

            foreach (var property in includeProperties.Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                result = result.Include(property);
            }

            return await result.FirstOrDefaultAsync();
        }

        public Task<IEnumerable<Student>> Get(Expression<Func<Student, bool>> filter = null)
        {
            throw new NotImplementedException();
        }
    }
}
