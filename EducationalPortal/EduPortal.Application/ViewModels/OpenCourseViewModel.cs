using System.Collections.Generic;

namespace EduPortal.Application.ViewModels
{
    public class OpenCourseViewModel
    {
        public IEnumerable<MaterialViewModel> Materials { get; set; }
        public int Percentage { get; set; }
    }
}
