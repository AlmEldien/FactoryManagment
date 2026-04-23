using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace FactoryManagment.Infrastructure.Persistence.Repositories;

public class UserRepository(UserManager<ApplicationUser> userManager) : IUserRepository
{
    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        var appUser = await userManager.FindByEmailAsync(email);
        return appUser != null ? appUser : null;
    }

    public async Task<bool> CheckPasswordAsync(string userId, string password)
    {
        var appUser = await userManager.FindByIdAsync(userId);
        if (appUser == null) return false;
        return await userManager.CheckPasswordAsync(appUser, password);
    }

    public async Task<IEnumerable<string>> GetRolesAsync(string userId)
    {
        var appUser = await userManager.FindByIdAsync(userId);
        if (appUser == null) return Enumerable.Empty<string>();
        return await userManager.GetRolesAsync(appUser);
    }
}
