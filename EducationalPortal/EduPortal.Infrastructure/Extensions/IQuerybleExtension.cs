using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduPortal.Domain;
using Microsoft.EntityFrameworkCore;

namespace EduPortal.Infrastructure.Extensions
{
    public static class IQuerybleExtension
    {
        public static async Task<PagedList<T>> ToPagedAsync<T>(this IQueryable<T> query, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 1 : pageSize;

            int count = 0;
            if (query != null)
            {
                count = await query.CountAsync();

                if (count > 0)
                {
                    query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                }
            }

            List<T> items = query == null ? new List<T>() : await query.ToListAsync();

            return new PagedList<T>(count, pageNumber, pageSize, items);
        }
    }
}
