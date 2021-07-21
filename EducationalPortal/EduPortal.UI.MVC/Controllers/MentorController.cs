using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Application.ViewModels;
using EduPortal.Domain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EduPortal.UI.MVC.Controllers
{
    [Authorize(Roles = "Mentor")]
    public class MentorController : Controller
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

        [HttpGet]
        public async Task<IActionResult> CreateArticle()
        {
            var skills = await _mentorService.GetSkillsAsync();

            var groups = new List<SelectListGroup>();

            foreach (var skill in skills)
            {
                groups.Add(new SelectListGroup(){Name = skill.Name});
            }
            
            ViewBag.Skills = new List<SelectListItem>();

            foreach (var group in groups)
            {
                for (int i = 0; i < 10; i++)
                {
                    ViewBag.Skills.Add(new SelectListItem() { Text = (i + 1).ToString(), Value = $"{group.Name},{i + 1}", Group = group });
                }
            }

            return View();
        }

        [HttpPost]
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
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                await _mentorService.CreateArticleAsync(model, userId);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                foreach (var error in ModelState["Error"].Errors)
                {
                    ModelState.AddModelError("", "что-то не так");
                }
            }

            var skills = await _mentorService.GetSkillsAsync();

            var groups = new List<SelectListGroup>();

            foreach (var skill in skills)
            {
                groups.Add(new SelectListGroup() { Name = skill.Name });
            }

            ViewBag.Skills = new List<SelectListItem>();

            foreach (var group in groups)
            {
                for (int i = 0; i < 10; i++)
                {
                    ViewBag.Skills.Add(new SelectListItem() { Text = (i + 1).ToString(), Value = $"{group.Name},{i + 1}", Group = group });
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateBook()
        {
            var skills = await _mentorService.GetSkillsAsync();

            var groups = new List<SelectListGroup>();

            foreach (var skill in skills)
            {
                groups.Add(new SelectListGroup() { Name = skill.Name });
            }

            ViewBag.Skills = new List<SelectListItem>();

            foreach (var group in groups)
            {
                for (int i = 0; i < 10; i++)
                {
                    ViewBag.Skills.Add(new SelectListItem() { Text = (i + 1).ToString(), Value = $"{group.Name},{i + 1}", Group = group });
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(CreateBookViewModel model)
        {
            
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Text) || string.IsNullOrEmpty(model.Format))
            {
                ModelState.AddModelError("", "Все поля должны быть заполнены");
            }

            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                await _mentorService.CreateBookAsync(model, userId);
                return RedirectToAction("Index", "Home");
            }

            var skills = await _mentorService.GetSkillsAsync();

            var groups = new List<SelectListGroup>();

            foreach (var skill in skills)
            {
                groups.Add(new SelectListGroup() { Name = skill.Name });
            }

            ViewBag.Skills = new List<SelectListItem>();

            foreach (var group in groups)
            {
                for (int i = 0; i < 10; i++)
                {
                    ViewBag.Skills.Add(new SelectListItem() { Text = (i + 1).ToString(), Value = $"{group.Name},{i + 1}", Group = group });
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateVideo()
        {
            var skills = await _mentorService.GetSkillsAsync();

            var groups = new List<SelectListGroup>();

            foreach (var skill in skills)
            {
                groups.Add(new SelectListGroup() { Name = skill.Name });
            }

            ViewBag.Skills = new List<SelectListItem>();

            foreach (var group in groups)
            {
                for (int i = 0; i < 10; i++)
                {
                    ViewBag.Skills.Add(new SelectListItem() { Text = (i + 1).ToString(), Value = $"{group.Name},{i + 1}", Group = group });
                }
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateVideo(CreateVideoViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Length.ToString()))
            {
                ModelState.AddModelError("", "Все поля должны быть заполнены");
            }

            if (ModelState.IsValid)
            {
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                await _mentorService.CreateVideoAsync(model, userId);
                return RedirectToAction("Index", "Home");
            }

            var skills = await _mentorService.GetSkillsAsync();

            var groups = new List<SelectListGroup>();

            foreach (var skill in skills)
            {
                groups.Add(new SelectListGroup() { Name = skill.Name });
            }

            ViewBag.Skills = new List<SelectListItem>();

            foreach (var group in groups)
            {
                for (int i = 0; i < 10; i++)
                {
                    ViewBag.Skills.Add(new SelectListItem() { Text = (i + 1).ToString(), Value = $"{group.Name},{i + 1}", Group = group });
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            var materials = await _mentorService.GetMaterialsAsync();
            ViewBag.Materials = new List<SelectListItem>();
            foreach (var material in materials)
            {
                ViewBag.Materials.Add(new SelectListItem(){Text = material.Name, Value = material.Id.ToString()});
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Description) ||
                model.MaterialIds == null)
            {
                ModelState.AddModelError("", "Все поля должны быть заполнены");
            }

            if (ModelState.IsValid)
            {
                string uniqueFileName = UploadedFile(model);
                var userId = (await _userManager.FindByNameAsync(User.Identity.Name)).Id;
                model.ImagePath = uniqueFileName;
                model.Materials = await _mentorService.GetMaterialsByIdAsync(model.MaterialIds.Select(s => int.Parse(s)));
                await _mentorService.CreateCourseAsync(model, userId);

                return RedirectToAction("Index", "Home");
            }
            

            var materials = await _mentorService.GetMaterialsAsync();
            ViewBag.Materials = new List<SelectListItem>();
            foreach (var material in materials)
            {
                ViewBag.Materials.Add(new SelectListItem() { Text = material.Name, Value = material.Id.ToString() });
            }

            return View(model);
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
