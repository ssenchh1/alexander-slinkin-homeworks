using System.Collections.Generic;
using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EduPortalWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ICourseService _courseService;

        public HomeController(ICourseService courseService, ILogger<HomeController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IEnumerable<CourseViewModel>> Index()
        {
            var courses = await  _courseService.GetTopCourses();

            return courses.Courses;
        }
    }
}
