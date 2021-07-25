using System.Collections.Generic;
using EduPortal.Domain.Models.Materials;

namespace EduPortal.Domain.Models.Users
{
    public class Mentor : IUser
    {
        public virtual User User { get; set; }
        public string UserId { get; set; }

        public List<Material> CreatedMaterials { get; set; }

        public List<Course> CreatedCourses { get; set; }
        public string Id { get; set; }
    }
}
