using System.Collections.Generic;
using EduPortal.Domain.Models;

namespace EduPortal.Application.ViewModels
{
    public class CoursesViewModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }
    }
}
