using System.Collections.Generic;
using EduPortal.Domain.Models.Materials;
using Microsoft.AspNetCore.Http;

namespace EduPortal.Application.ViewModels
{
    public class CreateCourseViewModel
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public List<string> MaterialIds { get; set; }

        public IEnumerable<Material> Materials { get; set; }

        public IFormFile CourseImage { get; set; }

        public string ImagePath { get; set; }
    }
}
