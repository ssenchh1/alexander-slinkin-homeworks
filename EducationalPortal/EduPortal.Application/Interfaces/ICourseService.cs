using System.Threading.Tasks;
using EduPortal.Application.ViewModels;
using EduPortal.Domain;
using EduPortal.Domain.Models;
using EduPortal.Domain.Models.Users;

namespace EduPortal.Application.Interfaces
{
    public interface ICourseService
    {
        Task<CoursesViewModel> GetCourses();

        Task<PagedList<CourseViewModel>> GetCoursesPaged(int pageNumber, int pageSize);

        Task<CoursesViewModel> GetTopCourses();

        Task<CourseViewModel> GetCourseVMById(int id, string include);

        Task<Course> GetCourseById(int id, string include);

        Task AddStudentToCourse(int courseId, Student student);

        Task AddCourseToStudent(int courseId, Student student);

        Task<bool> IsPurchased(int corseId, Student student);

        Task<bool> IsMaterialPassed(int materialId, string userName);

        Task<bool> IsMaterialPassed(int materialId, Student student);

        Task PassMaterial(int materialId, string userName);

        Task PassMaterial(int materialId, User user);

        Task AddSkills(int materialId, string userName);

        Task AddSkills(int materialId, User user);
    }
}
