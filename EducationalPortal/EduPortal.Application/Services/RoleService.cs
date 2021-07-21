using System.Threading.Tasks;
using EduPortal.Application.Interfaces;
using EduPortal.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;

namespace EduPortal.Application.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<User> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public async Task<bool> RoleExistsAsync(string roleName)
        {
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var role = new IdentityRole();
            role.Name = roleName;
            await _roleManager.CreateAsync(role);
        }

        public async Task SetUserRoleAsync(User user, string roleName)
        {
            if (!await RoleExistsAsync(roleName))
            {
                await CreateRoleAsync(roleName);
            }

            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task UnsetUserRoleAsync(User user, string roleName)
        {
            if (await RoleExistsAsync(roleName))
            {
                if (await _userManager.IsInRoleAsync(user, roleName))
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
        }
    }
}
