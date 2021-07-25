using System.Collections.Generic;
using EduPortal.Domain.Models.Joining;
using EduPortal.Domain.Models.Materials;
using EduPortal.Domain.Models.Users;

namespace EduPortal.Domain.Models
{
    public class Skill : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual List<Material> Materials { get; set; }

        public virtual List<User> Users { get; set; }

        public List<SkillMaterial> SkillMaterials { get; set; }

        public List<SkillUser> SkillUsers { get; set; }
    }
}
