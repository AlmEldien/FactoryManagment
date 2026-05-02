using FactoryManagment.Application.Dtos;
using FactoryManagment.Domain.Entities;

namespace FactoryManagment.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(string userId, string password);
    Task<IEnumerable<string>> GetRolesAsync(string userId);
    Task<(ApplicationUser? User, IReadOnlyList<string>? Errors)> CreateUserAsync(RegisterDto dto, CancellationToken cancellationToken = default);
    Task AddToRoleAsync(string userId, string role);
}
