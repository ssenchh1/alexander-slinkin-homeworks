using System;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Application.ViewModels;
using EduPortal.Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduPortalWebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Mentor")]
    public class MentorController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IMentorService _mentorService;
        private readonly UserManager<User> _userManager;

        public MentorController(IMentorService mentorService, UserManager<User> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _mentorService = mentorService;
            _userManager = userManager;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("CreateArticle")]
        public async Task<IActionResult> CreateArticle(CreateArticleViewModel model)
        {
            ModelState.Remove("Date");
            model.Date = DateTime.Today;

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Text) || string.IsNullOrEmpty(model.Source))
            {
                ModelState.AddModelError("Error", "Все поля должны быть заполнены");
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _mentorService.CreateArticleAsync(model, userId);
                return Ok();
            }
            else
            {
                foreach (var error in ModelState["Error"].Errors)
                {
                    ModelState.AddModelError("", "что-то не так");
                }
            }
            
            return BadRequest();
        }

        [HttpPost]
        [Route("CreateBook")]
        public async Task<IActionResult> CreateBook(CreateBookViewModel model)
        {

            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Text) || string.IsNullOrEmpty(model.Format))
            {
                ModelState.AddModelError("", "Все поля должны быть заполнены");
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _mentorService.CreateBookAsync(model, userId);
                return Ok();
            }


            return BadRequest();
        }

        [HttpPost]
        [Route("CreateVideo")]
        public async Task<ActionResult> CreateVideo(CreateVideoViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Length.ToString()))
            {
                ModelState.AddModelError("", "Все поля должны быть заполнены");
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _mentorService.CreateVideoAsync(model, userId);
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("CreateCourse")]
        public async Task<ActionResult> CreateCourse(CreateCourseViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Description) ||
                model.MaterialIds == null)
            {
                ModelState.AddModelError("", "Все поля должны быть заполнены");
            }

            if (ModelState.IsValid)
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                string uniqueFileName = UploadedFile(model);
                model.ImagePath = uniqueFileName;
                model.Materials = await _mentorService.GetMaterialsByIdAsync(model.MaterialIds.Select(s => int.Parse(s)));
                await _mentorService.CreateCourseAsync(model, userId);

                return Ok();
            }

            return BadRequest();
        }

        private string UploadedFile(CreateCourseViewModel model)
        {
            string uniqueFileName = null;

            if (model.CourseImage != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "img");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.CourseImage.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.CourseImage.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
