using System;
using System.Collections.Generic;

namespace EduPortal.Domain
{
    public class PagedList<T>
    {
        public int CurrentPageNumber { get; set; }
        public int TotalPages { get; set; }
        public IEnumerable<T> Items { get; set; }

        public PagedList(int count, int currentPageNumber, int pageSize, IEnumerable<T> items)
        {
            CurrentPageNumber = currentPageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public bool HasPreviousPage => (CurrentPageNumber > 1);

        public bool HasNextPage => (CurrentPageNumber < TotalPages);
    }
}
