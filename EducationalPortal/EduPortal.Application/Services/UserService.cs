using System.Collections.Generic;
using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Application.ViewModels;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models;
using EduPortal.Domain.Models.Users;

namespace EduPortal.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericUserRepository<User> _userRepository;
        private readonly IRepository<Skill> _skillRepository;

        public UserService(IGenericUserRepository<User> userRepository, IRepository<Skill> skillRepository)
        {
            _userRepository = userRepository;
            _skillRepository = skillRepository;
        }

        public async Task<Dictionary<string, int>> GetUserSkills(string userId)
        {
            var user = await _userRepository.GetByIdAsync(userId, "SkillUsers");
            var skillsUsers = user.SkillUsers;

            var skills = new Dictionary<string, int>();
            foreach (var skillsUser in skillsUsers)
            {
                var skill = await _skillRepository.GetByIdAsync(skillsUser.SkillId);
                skills.Add(skill.Name, skillsUser.Level);
            }

            return skills;
        }

        public async Task<UserProfileViewModel> GetProfile(string userId, AccountCoursesViewModel courses)
        {
            var user = await _userRepository.GetByIdAsync(userId, "SkillUsers");

            var skills = await GetUserSkills(userId);

            return new UserProfileViewModel() { Login = user.Login, Email = user.Email, Courses = courses.Courses, FinishedCourses = courses.FinishedCourses, Skills = skills };
        }
    }
}
