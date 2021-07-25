using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Application.ViewModels;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models;
using EduPortal.Domain.Models.Joining;
using EduPortal.Domain.Models.Materials;

namespace EduPortal.Application.Services
{
    public class MentorService : IMentorService
    {
        private readonly IRepository<Material> _materialRepository;
        private readonly IRepository<Course> _courseRepository;
        private readonly IRepository<Skill> _skillRepository;

        public MentorService(IRepository<Material> materialRepository, IRepository<Course> courseRepository, IRepository<Skill> skillRepository)
        {
            _materialRepository = materialRepository;
            _courseRepository = courseRepository;
            _skillRepository = skillRepository;
        }

        public async Task CreateArticleAsync(CreateArticleViewModel model, string authorId)
        {
            var article = new Article()
            {
                Name = model.Name, Text = model.Text, Source = model.Source, ProvidedSkills = model.ProvidedSkills, Category = model.Category, Date = model.Date, AuthorId = authorId, SkillMaterials = new List<SkillMaterial>()
            };
            
            await _materialRepository.AddAsync(article);

            foreach (var modelSkillsPoint in model.SkillsPoints)
            {
                var skillname = modelSkillsPoint.Split(',')[0];
                var level = int.Parse(modelSkillsPoint.Split(',')[1]);
                var skill = (await _skillRepository.GetAsync(s => s.Name == skillname)).FirstOrDefault();

                article.SkillMaterials.Add(new SkillMaterial() { MaterialId = article.Id, SkillId = skill.Id, Level = level });
            }

            await _materialRepository.UpdateAsync(article);
        }

        public async Task CreateBookAsync(CreateBookViewModel model, string authorId)
        {
            var book = new DigitalBook()
            {
                Name = model.Name, Format = model.Format, Text  = model.Text, NumberOfPages = model.NumberOfPages, Year = model.Year,
                Category = model.Category, ProvidedSkills = model.ProvidedSkills, AuthorId = authorId, SkillMaterials = new List<SkillMaterial>()
            };

            await _materialRepository.AddAsync(book);

            foreach (var modelSkillsPoint in model.SkillsPoints)
            {
                var skillname = modelSkillsPoint.Split(',')[0];
                var level = int.Parse(modelSkillsPoint.Split(',')[1]);
                var skill = (await _skillRepository.GetAsync(s => s.Name == skillname)).FirstOrDefault();

                book.SkillMaterials.Add(new SkillMaterial() { MaterialId = book.Id, SkillId = skill.Id, Level = level });
            }

            await _materialRepository.UpdateAsync(book);
        }

        public async Task CreateVideoAsync(CreateVideoViewModel model, string authorId)
        {
            var video = new VideoMaterial()
            {
                Name = model.Name, Category = model.Category, Length = model.Length, ProvidedSkills = model.ProvidedSkills, AuthorId = authorId, SkillMaterials = new List<SkillMaterial>()
            };

            await _materialRepository.AddAsync(video);

            foreach (var modelSkillsPoint in model.SkillsPoints)
            {
                var skillname = modelSkillsPoint.Split(',')[0];
                var level = int.Parse(modelSkillsPoint.Split(',')[1]);
                var skill = (await _skillRepository.GetAsync(s => s.Name == skillname)).FirstOrDefault();

                video.SkillMaterials.Add(new SkillMaterial() { MaterialId = video.Id, SkillId = skill.Id, Level = level });
            }

            await _materialRepository.UpdateAsync(video);
        }

        public async Task<IEnumerable<Material>> GetMaterialsAsync()
        {
            return await _materialRepository.GetAsync();
        }

        public async Task<IEnumerable<Skill>> GetSkillsAsync()
        {
            return await _skillRepository.GetAsync();
        }

        public async Task<IEnumerable<Material>> GetMaterialsByIdAsync(IEnumerable<int> ids)
        {
            return await _materialRepository.GetAsync(m => ids.Contains(m.Id));
        }

        public async Task CreateCourseAsync(CreateCourseViewModel model, string authorId)
        {
            var course = new Course()
                {Name = model.Name, Description = model.Description, Materials = model.Materials.ToList(), AuthorId = authorId, CourseImage = model.ImagePath};

            await _courseRepository.AddAsync(course);
        }

        public Task UpdateCourseAsync(CreateCourseViewModel model, string authorId)
        {
            throw new NotImplementedException();
        }
    }
}
