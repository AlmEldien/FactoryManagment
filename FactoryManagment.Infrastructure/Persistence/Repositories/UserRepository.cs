using FactoryManagment.Application.Abstractions.Repositories;
using FactoryManagment.Domain.Entities;
using FactoryManagment.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace FactoryManagment.Infrastructure.Persistence.Repositories;

public class UserRepository(UserManager<ApplicationUser> userManager) : IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        var appUser = await userManager.FindByEmailAsync(email);
        return appUser != null ? MapToDomainUser(appUser) : null;
    }

    public async Task<bool> CheckPasswordAsync(User user, string password)
    {
        var appUser = await userManager.FindByIdAsync(user.Id);
        if (appUser == null) return false;
        return await userManager.CheckPasswordAsync(appUser, password);
    }

    public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
    {
        var appUser = userManager.Users.FirstOrDefault(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
        return appUser != null ? MapToDomainUser(appUser) : null;
    }

    public async Task<bool> IsRefreshTokenActiveAsync(string refreshToken)
    {
        var appUser = userManager.Users.FirstOrDefault(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
        if (appUser == null) return false;

        var token = appUser.RefreshTokens.FirstOrDefault(t => t.Token == refreshToken);
        return token != null && token.IsActive;
    }

    public async Task RevokeRefreshTokenAsync(string refreshToken)
    {
        var appUser = userManager.Users.FirstOrDefault(u => u.RefreshTokens.Any(t => t.Token == refreshToken));
        if (appUser != null)
        {
            var token = appUser.RefreshTokens.FirstOrDefault(t => t.Token == refreshToken);
            if (token != null)
            {
                token.Revoke();
                await userManager.UpdateAsync(appUser);
            }
        }
    }

    public async Task AddRefreshTokenAsync(User user, RefreshToken refreshToken)
    {
        var appUser = await userManager.FindByIdAsync(user.Id);
        if (appUser != null)
        {
            appUser.RefreshTokens.Add(refreshToken);
            await userManager.UpdateAsync(appUser);
        }
    }

    public async Task<IEnumerable<string>> GetRolesAsync(User user)
    {
        var appUser = await userManager.FindByIdAsync(user.Id);
        if (appUser == null) return Enumerable.Empty<string>();
        return await userManager.GetRolesAsync(appUser);
    }

    private static User MapToDomainUser(ApplicationUser appUser)
    {
        return new User(appUser.Id, appUser.FirstName, appUser.LastName, appUser.UserName!, appUser.Email!, appUser.PhoneNumber ?? string.Empty);
    }
}
