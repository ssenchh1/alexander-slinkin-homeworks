using EduPortal.UI.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;
using EduPortal.Application.Interfaces;

namespace EduPortal.UI.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ICourseService _courseService;

        public HomeController(ICourseService courseService, ILogger<HomeController> logger)
        {
            _courseService = courseService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await  _courseService.GetTopCourses();

            return View(courses);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
