using System.Collections.Generic;

namespace EduPortal.Application.ViewModels
{
    public class UserProfileViewModel
    {
        public string Login { get; set; }

        public string Email { get; set; }

        public IEnumerable<CourseViewModel> Courses { get; set; }

        public IEnumerable<CourseViewModel> FinishedCourses { get; set; }
    }
}
