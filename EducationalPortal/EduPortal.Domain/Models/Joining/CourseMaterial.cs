using EduPortal.Domain.Models.Materials;

namespace EduPortal.Domain.Models.Joining
{
    public class CourseMaterial
    {
        public Course Course { get; set; }
        public int CourseId { get; set; }

        public Material Material { get; set; }
        public int MaterialId { get; set; }
    }
}
