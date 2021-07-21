using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models.Users;
using EduPortal.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Infrastructure.Repositories
{
    public class MentorRepository : IUserRepository<Mentor>
    {
        private EducationalPortalContext db;

        public MentorRepository(EducationalPortalContext dbcontext)
        {
            db = dbcontext;
        }


        public async Task AddAsync(Mentor obj)
        {
            await db.Mentors.AddAsync(obj);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Mentor obj)
        {
            db.Mentors.Update(obj);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Mentor obj)
        {
            db.Mentors.Remove(obj);
            await db.SaveChangesAsync();
        }

        public async Task<Mentor> GetByIdAsync(string id)
        {
            return await db.Mentors.FirstAsync(m => m.Id == id);
        }

        public Task<Mentor> GetByIdAsync(string id, string includeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Mentor> Get(Expression<Func<Mentor, bool>> filter)
        {
            IQueryable<Mentor> query = db.Mentors;

            query = query.Where(filter).Include(m => m.CreatedCourses);

            return query;
        }

        Task<IEnumerable<Mentor>> IUserRepository<Mentor>.Get(Expression<Func<Mentor, bool>> filter)
        {
            throw new NotImplementedException();
        }
    }
}
