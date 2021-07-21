using System.Collections.Generic;
using EduPortal.Domain.Models;

namespace EduPortal.Application.ViewModels
{
    public class CreateBookViewModel
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string Text { get; set; }

        public int NumberOfPages { get; set; }

        public string Format { get; set; }

        public int Year { get; set; }

        public List<string> SkillsPoints { get; set; }

        public List<Skill> ProvidedSkills { get; set; }
    }
}
