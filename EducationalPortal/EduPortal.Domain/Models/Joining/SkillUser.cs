using EduPortal.Domain.Models.Users;

namespace EduPortal.Domain.Models.Joining
{
    public class SkillUser
    {
        public int SkillId { get; set; }
        public Skill Skill { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }

        public int Level { get; set; }
    }
}
