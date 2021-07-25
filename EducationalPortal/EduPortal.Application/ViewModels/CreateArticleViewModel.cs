using System;
using System.Collections.Generic;
using EduPortal.Domain.Models;

namespace EduPortal.Application.ViewModels
{
    public class CreateArticleViewModel
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public string Text { get; set; }

        public string Source { get; set; }

        public List<string> SkillsPoints { get; set; }

        public List<Skill> ProvidedSkills { get; set; }

        public DateTime Date { get; set; }
    }
}
