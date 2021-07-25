using System.Collections.Generic;
using EduPortal.Domain.Models.Joining;
using Microsoft.AspNetCore.Identity;

namespace EduPortal.Domain.Models.Users
{
    public class User : IdentityUser
    {
        public virtual Mentor Mentor { get; set; }
        public virtual Student Student { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public virtual List<Skill> Skills { get; set; }

        public List<SkillUser> SkillUsers { get; set; }

        public string Role { get; set; }

        public string ProfilePicture { get; set; }
    }
}
