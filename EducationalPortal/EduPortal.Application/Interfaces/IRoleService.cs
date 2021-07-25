using System.Threading.Tasks;
using EduPortal.Domain.Models.Users;

namespace EduPortal.Application.Interfaces
{
    public interface IRoleService
    {
        Task<bool> RoleExistsAsync(string roleName);

        Task CreateRoleAsync(string roleName);

        Task SetUserRoleAsync(User user, string roleName);

        Task UnsetUserRoleAsync(User user, string roleName);
    }
}
