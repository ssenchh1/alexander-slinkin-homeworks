using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Application.ViewModels;
using EduPortal.Domain.Interfaces;
using EduPortal.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EduPortalWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRoleService _roleService;
        private readonly IUserRepository<Student> _studentRepository;
        private readonly IUserRepository<Mentor> _mentorRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IRoleService roleService, IUserRepository<Student> studentRepository, IUserRepository<Mentor> mentorRepository)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleService = roleService;
            _studentRepository = studentRepository;
            _mentorRepository = mentorRepository;
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (model.Password != model.PasswordConfirm)
            {
                ModelState.AddModelError("", "Пароли должны совпадать");
            }

            if (!new EmailAddressAttribute().IsValid(model.Email))
            {
                ModelState.AddModelError("", "Неверный адрес электронной почты");
            }

            if (ModelState.IsValid)
            {
                var user = new User() { Email = model.Email, Password = model.Password, UserName = model.Email, Login = model.Login, Role = "Student", ProfilePicture = "default-avatar.jpg" };

                var result = await _userManager.CreateAsync(user, user.Password);

                if (result.Succeeded)
                {
                    await _studentRepository.AddAsync(new Student() { Id = user.Id, UserId = user.Id });
                    await _signInManager.SignInAsync(user, model.RememberMe);
                    await _roleService.SetUserRoleAsync(user, "Student");
                    return Ok();
                }
                else
                {
                    foreach (var identityError in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, identityError.Description);
                    }
                }
            }

            return BadRequest();
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
            {
                ModelState.AddModelError("", "Все поля должны быть заполнены");
            }

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.Email);
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    return Ok();
                }
                else
                {
                    ModelState.AddModelError("", "Неправильные данные");
                }
            }

            return BadRequest();
        }

        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> ChangeRole(string id, string oldRole, string newRole)
        {
            var user = await _userManager.FindByIdAsync(id);
            var student = await _studentRepository.GetByIdAsync(id);
            var mentor = new Mentor() { Id = user.Id, UserId = user.Id };
            await _mentorRepository.AddAsync(mentor);
            await _roleService.SetUserRoleAsync(user, newRole);
            await _roleService.UnsetUserRoleAsync(user, oldRole);
            await _signInManager.RefreshSignInAsync(user);
            return Ok();
        }
    }
}
