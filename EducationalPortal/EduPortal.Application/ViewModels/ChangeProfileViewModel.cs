using Microsoft.AspNetCore.Http;

namespace EduPortal.Application.ViewModels
{
    public class ChangeProfileViewModel
    {
        public string Login { get; set; }

        public IFormFile ProfileImage { get; set; }

        public string ImagePath { get; set; }
    }
}
