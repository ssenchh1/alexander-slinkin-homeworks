using EduPortal.Domain.Models.Users;

namespace EduPortal.Application.Interfaces
{
    public interface IJwtGenerator
    {
        string CreateToken(User user);
    }
}
