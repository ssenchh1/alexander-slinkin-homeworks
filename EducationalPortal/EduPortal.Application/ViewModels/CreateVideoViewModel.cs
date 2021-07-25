using System.Collections.Generic;
using EduPortal.Domain.Models;

namespace EduPortal.Application.ViewModels
{
    public class CreateVideoViewModel
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public int Length { get; set; }

        public List<string> SkillsPoints { get; set; }

        public List<Skill> ProvidedSkills { get; set; }
    }
}
