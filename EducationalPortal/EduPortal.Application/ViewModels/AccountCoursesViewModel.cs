using System.Collections.Generic;

namespace EduPortal.Application.ViewModels
{
    public class AccountCoursesViewModel
    {
        public IEnumerable<CourseViewModel> Courses { get; set; }

        public IEnumerable<CourseViewModel> FinishedCourses { get; set; }
    }
}
