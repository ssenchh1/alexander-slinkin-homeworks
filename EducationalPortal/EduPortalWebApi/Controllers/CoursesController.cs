using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Application.ViewModels;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models.Materials;
using EduPortal.Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduPortalWebApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CoursesController : ControllerBase
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
        [AllowAnonymous]
        [Route("Catalog")]
        public async Task<IEnumerable<CourseViewModel>> Catalog(int? page)
        {
            int currentPage = page ?? 1;
            int pageSize = 3;

            var courses = await _courseService.GetCoursesPaged(currentPage, pageSize);

            return courses.Items;
        }

        [HttpGet]
        [Route("Course/{id?}")]
        [Authorize]
        public async Task<ActionResult<CourseViewModel>> Course(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Catalog", "Courses");
            }

            var course = await _courseService.GetCourseVMById((int)id, "Students");
            return course;
        }

        [HttpPost]
        [Route("PurchaseCourse/{id}")]
        public async Task<ActionResult> PurchaseCourse(int id)
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var student = await _studentRepository.GetByIdAsync(studentId);
            await _courseService.AddStudentToCourse(id, student);
            return Content("Success");
        }

        [HttpGet]
        [Route("OpenCourse/{courseId}")]
        public async Task<ActionResult<OpenCourseViewModel>> OpenCourse(int courseId)
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var student = await _studentRepository.GetByIdAsync(studentId, "PassedMaterials");
            if (await _courseService.IsPurchased(courseId, student))
            {
                var course = await _courseService.GetCourseById(courseId, "Students,Materials");
                var materials = course.Materials;
                int countPassed = 0;
                foreach (var material in materials)
                {
                    var result = await _courseService.IsMaterialPassed(material.Id, student);
                    if (result)
                    {
                        countPassed++;
                    }
                }

                var percent = countPassed / materials.Count * 100;
                var Vm = new List<MaterialViewModel>();

                foreach (var material in materials)
                {
                    if (material is Article article)
                    {
                        var Vmodel = new ArticleViewModel()
                        {
                            AuthorId = article.AuthorId, Date = article.Date, Id = article.Id, Name = article.Name,
                            Source = article.Source, Text = article.Text
                        };
                        Vm.Add(Vmodel);
                    }

                    if (material is DigitalBook book)
                    {
                        var Vmodel = new BookViewModel()
                        {
                            AuthorId = book.AuthorId, Id = book.Id, Name = book.Name, Text = book.Text,
                            Format = book.Format, NumberOfPages = book.NumberOfPages, Year = book.Year
                        };
                        Vm.Add(Vmodel);
                    }

                    if (material is VideoMaterial video)
                    {
                        var Vmodel = new VideoMaterialViewModel()
                        {
                            AuthorId = video.AuthorId, Id = video.Id, Name = video.Name, Length = video.Length
                        };
                        Vm.Add(Vmodel);
                    }
                }

                var model = new OpenCourseViewModel()
                {
                    Percentage = percent,
                    Materials = Vm
                };

                return model;
            }

            return Ok();
        }

        [HttpPost]
        [Route("ApproveMaterial/{materialId}")]
        public async Task<ActionResult> ApproveMaterial(int materialId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var user = await _userManager.FindByIdAsync(userId);
            var task1 = _courseService.PassMaterial(materialId, user);

            Task.WaitAll(task1);

            var task2 = _courseService.AddSkills(materialId, user);

            return Ok();
        }
    }
}
