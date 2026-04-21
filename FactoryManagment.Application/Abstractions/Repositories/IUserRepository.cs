using FactoryManagment.Domain.Entities;

namespace FactoryManagment.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
    Task<bool> IsRefreshTokenActiveAsync(string refreshToken);
    Task RevokeRefreshTokenAsync(string refreshToken);
    Task AddRefreshTokenAsync(User user, RefreshToken refreshToken);
    Task<IEnumerable<string>> GetRolesAsync(User user);
}
