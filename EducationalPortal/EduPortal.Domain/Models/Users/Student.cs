using System.Collections.Generic;
using EduPortal.Domain.Models.Joining;
using EduPortal.Domain.Models.Materials;

namespace EduPortal.Domain.Models.Users
{
    public class Student : IUser
    {
        public virtual User User { get; set; }
        public string UserId { get; set; }

        public List<Course> Courses { get; set; }

        public List<Material> PassedMaterials { get; set; }

        public List<Course> FinishedCourses { get; set; }

        public List<StudentCourse> StudentCourses { get; set; }
        //public List<MaterialPassedStudents> MaterialPassedStudents { get; set; }
        public List<StudentFinishedCourse> StudentFinishedCourses { get; set; }
        public string Id { get; set; }
    }
}
