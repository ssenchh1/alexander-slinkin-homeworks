using System.Collections.Generic;
using System.Threading.Tasks;
using EduPortal.Application.ViewModels;

namespace EduPortal.Application.Interfaces
{
    public interface IUserService
    {
        Task<Dictionary<string, int>> GetUserSkills(string userId);

        Task<UserProfileViewModel> GetProfile(string userId, AccountCoursesViewModel courses);
    }
}
