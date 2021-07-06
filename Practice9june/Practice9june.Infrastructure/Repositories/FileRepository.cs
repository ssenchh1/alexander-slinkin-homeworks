using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Practice9june.Core.Interfaces;
using Practice9june.Core.Models;

namespace Practice9june.Infrastructure.Repositories
{
    public class FileRepository : IRepository<File>
    {
        private FileSystemContext db;

        public FileRepository(FileSystemContext context)
        {
            db = context;
        }

        public void Add(File obj)
        {
            db.Files.Add(obj);
            db.SaveChanges();
        }

        public void Delete(File obj)
        {
            db.Files.Remove(obj);
            db.SaveChanges();
        }

        public IEnumerable<File> Get(Expression<Func<File, bool>> filter = null)
        {
            return filter == null ? db.Files : db.Files.Where(filter);
        }
    }
}
