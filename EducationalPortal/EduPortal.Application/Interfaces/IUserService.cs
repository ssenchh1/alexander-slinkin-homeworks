using System.Collections.Generic;
using System.Threading.Tasks;
using EduPortal.Application.ViewModels;

namespace EduPortal.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<CourseViewModel>> GetUserCourses(string userId);

        Task<IEnumerable<CourseViewModel>> GetFinishedCourses(string userId);

        Task<UserProfileViewModel> GetProfile(string userId);
    }
}
