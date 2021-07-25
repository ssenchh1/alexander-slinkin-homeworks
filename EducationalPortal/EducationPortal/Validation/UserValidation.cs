using EduPortal.Domain.Models.Users;
using FluentValidation;

namespace EducationPortal.Validation
{
    class UserValidation : AbstractValidator<User>
    {
        public UserValidation()
        {
            RuleFor(u => u.Login).NotEmpty().Length(4, 30);
            RuleFor(u => u.Password).NotEmpty().Length(4, 20);
        }
    }
}
