using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Application.ViewModels;
using EduPortal.Domain;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models.Users;
using EduPortal.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace EduPortalWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository<Student> _studentRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserService _userService;

        public UserController(IUserRepository<Student> studentRepository, IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        [HttpGet]
        [Route("Profile")]
        public async Task<ActionResult<UserProfileViewModel>> MyProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var courses = await Courses();

            return await _userService.GetProfile(userId, courses);
        }

        [HttpGet]
        [Route("Courses")]
        public async Task<AccountCoursesViewModel> Courses()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var student = await _studentRepository.GetByIdAsync(userId, "Courses,FinishedCourses");

            List<CourseViewModel> inProgressCourses = new List<CourseViewModel>();
            List<CourseViewModel> finishedCourses = new List<CourseViewModel>();

            foreach (var studentCourse in student.Courses)
            {
                inProgressCourses.Add(new CourseViewModel(){Author = studentCourse.AuthorId, Description = studentCourse.Description, Id = studentCourse.Id, ImagePath = studentCourse.CourseImage, StudentsCount = studentCourse.Students.Count, Title = studentCourse.Name});
            }

            foreach (var studentCourse in student.FinishedCourses)
            {
                finishedCourses.Add(new CourseViewModel() { Author = studentCourse.AuthorId, Description = studentCourse.Description, Id = studentCourse.Id, ImagePath = studentCourse.CourseImage, StudentsCount = studentCourse.Students.Count, Title = studentCourse.Name });
            }

            AccountCoursesViewModel model = new AccountCoursesViewModel()
            {
                Courses = inProgressCourses,
                FinishedCourses = finishedCourses
            };

            return model;
        }

        [HttpGet]
        [Route("InProgressCourses")]
        public async Task<PagedList<CourseViewModel>> InProgressCourses(int? page)
        {
            int currentPage = page ?? 1;
            int pageSize = 3;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var student = await _studentRepository.GetByIdAsync(userId, "Courses,FinishedCourses");

            var result = student.Courses.ToPagedAsync(currentPage, pageSize);

            var items = new List<CourseViewModel>();

            foreach (var resultItem in result.Items)
            {
                items.Add(new CourseViewModel(){Author = resultItem.AuthorId, Description = resultItem.Description, Id = resultItem.Id, ImagePath = resultItem.CourseImage, StudentsCount = resultItem.Students.Count, Title = resultItem.Name});
            }

            var model = new PagedList<CourseViewModel>(student.Courses.Count, currentPage, pageSize, items);

            return model;
        }

        [HttpGet]
        [Route("FinishedCourses")]
        public async Task<PagedList<CourseViewModel>> FinishedCourses(int? page)
        {
            int currentPage = page ?? 1;
            int pageSize = 3;

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var student = await _studentRepository.GetByIdAsync(userId, "Courses,FinishedCourses");

            var result = student.FinishedCourses.ToPagedAsync(currentPage, pageSize);

            var items = new List<CourseViewModel>();

            foreach (var resultItem in result.Items)
            {
                items.Add(new CourseViewModel() { Author = resultItem.AuthorId, Description = resultItem.Description, Id = resultItem.Id, ImagePath = resultItem.CourseImage, StudentsCount = resultItem.Students.Count, Title = resultItem.Name });
            }

            var model = new PagedList<CourseViewModel>(student.Courses.Count, currentPage, pageSize, items);

            return model;
        }

        //[HttpPost]
        //[Route("ChangeProfile")]
        //public async Task<IActionResult> ChangeProfile(ChangeProfileViewModel model)
        //{
        //    string uniqueFileName = UploadedFile(model);

        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);

        //    if (model.Login != null)
        //    {
        //        user.Login = model.Login;
        //    }
        //    user.ProfilePicture = uniqueFileName;
        //    await _userManager.UpdateAsync(user);
        //    return RedirectToAction("MyProfile", "User");
        //}

        private string UploadedFile(ChangeProfileViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfileImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfileImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfileImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
