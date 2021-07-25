using EduPortal.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPortal.Infrastructure.Extensions
{
    public static class IEnumerableExtension
    {
        public static PagedList<T> ToPagedAsync<T>(this IEnumerable<T> collection, int pageNumber, int pageSize)
        {
            pageNumber = pageNumber < 1 ? 1 : pageNumber;
            pageSize = pageSize < 1 ? 1 : pageSize;

            int count = 0;
            if (collection != null)
            {
                count = collection.Count();

                if (count > 0)
                {
                    collection = collection.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                }
            }

            List<T> items = collection == null ? new List<T>() : collection.ToList();

            return new PagedList<T>(count, pageNumber, pageSize, items);
        }
    }
}
