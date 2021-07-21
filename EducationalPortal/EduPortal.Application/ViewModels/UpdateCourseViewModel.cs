using System.Collections.Generic;
using EduPortal.Domain.Models.Materials;

namespace EduPortal.Application.ViewModels
{
    public class UpdateCourseViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Material> Materials { get; set; }
    }
}
