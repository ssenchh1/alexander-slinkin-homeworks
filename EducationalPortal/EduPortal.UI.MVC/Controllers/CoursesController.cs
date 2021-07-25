using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduPortal.UI.MVC.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository<Student> _studentRepository;

        public CoursesController(ICourseService courseService, IUserRepository<Student> studentRepository, UserManager<User> userManager)
        {
            _courseService = courseService;
            _studentRepository = studentRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Catalog(int? page)
        {
            int currentPage = page ?? 1;
            int pageSize = 3;

            var courses = await _courseService.GetCoursesPaged(currentPage, pageSize);

            return View(courses);
        }

        [Authorize]
        public async Task<IActionResult> Course(int? id)
        {
            if (id == null)
            {
                RedirectToAction("Catalog", "Courses");
            }

            var student = await _studentRepository.GetByIdAsync((await _userManager.FindByNameAsync(User.Identity.Name)).Id);

            var course = await _courseService.GetCourseVMById((int)id, "Students");
            ViewBag.isPurchased = await _courseService.IsPurchased((int) id, student);
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> PurchaseCourse(int id)
        {
            var student = await _studentRepository.GetByIdAsync((await _userManager.FindByNameAsync(User.Identity.Name)).Id);
            await _courseService.AddStudentToCourse(id, student);
            return RedirectToAction("InProgressCourses", "User");
        }

        public async Task<IActionResult> OpenCourse(int id)
        {
            var student = await _studentRepository.GetByIdAsync((await _userManager.FindByNameAsync(User.Identity.Name)).Id);
            if (await _courseService.IsPurchased(id, student))
            {
                ViewData["courseId"] = id;
                var course = await _courseService.GetCourseById(id, "Students,Materials");
                var materials = course.Materials;
                int countPassed = 0;
                foreach (var material in materials)
                {
                    var result = await _courseService.IsMaterialPassed(material.Id, User.Identity.Name);
                    ViewData[material.Id.ToString()] = result;
                    if (result)
                    {
                        countPassed++;
                    }
                }

                var percent = countPassed / materials.Count * 100;
                ViewData["Percent"] = percent;
                return View(materials);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> ApproveMaterial(int materialId, int courseId)
        {
            var task1 = _courseService.PassMaterial(materialId, User.Identity.Name);

            Task.WaitAll(task1);

            var task2 = _courseService.AddSkills(materialId, User.Identity.Name);

            return RedirectToAction("OpenCourse", "Courses", new {id = courseId});
        }
    }
}
