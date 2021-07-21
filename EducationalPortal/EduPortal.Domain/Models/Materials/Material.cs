using System.Collections.Generic;
using EduPortal.Domain.Models.Joining;
using EduPortal.Domain.Models.Users;

namespace EduPortal.Domain.Models.Materials
{
    public class Material : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public string AuthorId { get; set; }

        public Mentor Author { get; set; }

        public virtual List<Skill> ProvidedSkills { get; set; }

        public List<SkillMaterial> SkillMaterials { get; set; }

        public List<Course> Courses { get; set; }

        public List<CourseMaterial> CourseMaterials { get; set; }

        public List<Student> PassedStudents { get; set; }

        //public List<MaterialPassedStudents> MaterialPassedStudents { get; set; }
    }
}
