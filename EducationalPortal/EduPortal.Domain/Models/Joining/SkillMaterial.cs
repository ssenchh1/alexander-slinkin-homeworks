using EduPortal.Domain.Models.Materials;

namespace EduPortal.Domain.Models.Joining
{
    public class SkillMaterial
    {
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        public int MaterialId { get; set; }
        public Material Material { get; set; }

        public int Level { get; set; }
    }
}
