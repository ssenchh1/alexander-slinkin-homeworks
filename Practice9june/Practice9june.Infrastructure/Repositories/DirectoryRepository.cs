using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Practice9june.Core.Interfaces;
using Practice9june.Core.Models;

namespace Practice9june.Infrastructure.Repositories
{
    public class DirectoryRepository : IRepository<Directory>
    {
        private FileSystemContext db;

        public DirectoryRepository(FileSystemContext context)
        {
            db = context;
        }

        public void Add(Directory obj)
        {
            db.Directories.Add(obj);
            db.SaveChanges();
        }

        public void Delete(Directory obj)
        {
            db.Directories.Remove(obj);
            db.SaveChanges();
        }

        public Directory GetById(int id)
        {
            return db.Directories.First(d => d.Id == id);
        }

        public IEnumerable<Directory> Get(Expression<Func<Directory, bool>> filter = null)
        {
            var toReturn = filter == null ? db.Directories : db.Directories.Where(filter);

            toReturn = toReturn.Include(d => d.Files)
                .Include(d => d.Directories)
                .ThenInclude(d => d.DirectoryPermissions);

            return toReturn;
        }
    }
}
