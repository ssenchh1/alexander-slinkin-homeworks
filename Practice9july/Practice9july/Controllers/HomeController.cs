using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Practice9july.Models;
using System.Diagnostics;
using Microsoft.Extensions.Configuration;

namespace Practice9july.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }
        
        [Route("Home")]
        [Route("Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
        
        [Route("/")]
        public IActionResult RefToHello()
        {
            return View();
        }

        [Route("/Hello")]
        public IActionResult Hello()
        {
            return Content("Hello " + _configuration["Author:Name"]);
        }

        [Route("/Hello.json")]
        public JsonResult JsonHello()
        {
            var result = new {data = "Hello " + _configuration["Author:Name"]};
            return Json(result);
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
