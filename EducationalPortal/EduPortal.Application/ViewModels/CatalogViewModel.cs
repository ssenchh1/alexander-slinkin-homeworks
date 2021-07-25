using System.Collections.Generic;
using EduPortal.Domain;

namespace EduPortal.Application.ViewModels
{
    public class CatalogViewModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }

        public PagedList<CourseViewModel> PagedList { get; set; }
    }
}
