using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Practice9july.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(50, ErrorMessage = "Имя не должно превышать 50 символов")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Фамилия не должна превышать 50 символов")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "Неверный адрес электронной почты")]
        public string Email { get; set; }

        [Required]
        [Compare("Email", ErrorMessage = "Адреса почт не совпадают")]
        public string EmailConfirm { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли должны совпадать")]
        public string PasswordConfirm { get; set; }
    }
}
