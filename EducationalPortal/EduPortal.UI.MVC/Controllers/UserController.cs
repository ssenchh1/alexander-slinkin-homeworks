using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EduPortal.Application.ViewModels;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models;
using EduPortal.Domain.Models.Users;
using EduPortal.Infrastructure.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace EduPortal.UI.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository<Student> _studentRepository;
        private readonly UserManager<User> _userManager;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public UserController(IUserRepository<Student> studentRepository, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _studentRepository = studentRepository;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize]
        public async Task<IActionResult> MyProfile()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var student = await _studentRepository.GetByIdAsync(user.Id);
            return View(user);
        }

        public async Task<IActionResult> Courses()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var student = await _studentRepository.GetByIdAsync(user.Id, "Courses,FinishedCourses");

            AccountCoursesViewModel model = new AccountCoursesViewModel()
            {
                Courses = student.Courses,
                FinishedCourses = student.FinishedCourses
            };

            return PartialView(model);
        }

        public async Task<IActionResult> InProgressCourses(int? page)
        {
            int currentPage = page ?? 1;
            int pageSize = 3;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var student = await _studentRepository.GetByIdAsync(user.Id, "Courses,FinishedCourses");

            var result = student.Courses.ToPagedAsync(currentPage, pageSize);

            return View(result);
        }

        public async Task<IActionResult> FinishedCourses(int? page)
        {
            int currentPage = page ?? 1;
            int pageSize = 3;

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var student = await _studentRepository.GetByIdAsync(user.Id, "Courses,FinishedCourses");

            var result = student.FinishedCourses.ToPagedAsync(currentPage, pageSize);

            return View("InProgressCourses", result);
        }

        [HttpGet]
        public IActionResult ChangeProfile()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangeProfile(ChangeProfileViewModel model)
        {
            string uniqueFileName = UploadedFile(model);

            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (model.Login != null)
            {
                user.Login = model.Login;
            }
            user.ProfilePicture = uniqueFileName;
            await _userManager.UpdateAsync(user);
            return RedirectToAction("MyProfile", "User");
        }

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
