using System.Collections.Generic;
using EduPortal.Domain.Models.Joining;
using EduPortal.Domain.Models.Materials;
using EduPortal.Domain.Models.Users;

namespace EduPortal.Domain.Models
{
    public class Course : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string CourseImage { get; set; }

        public string AuthorId { get; set; }

        public virtual Mentor Author { get; set; }

        public virtual List<Material> Materials { get; set; }

        public List<CourseMaterial> CourseMaterials { get; set; }

        public virtual List<Student> Students { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }

        public virtual List<Student> FinishedStudents { get; set; }

        public List<StudentFinishedCourse> StudentFinishedCourses { get; set; }
    }
}
