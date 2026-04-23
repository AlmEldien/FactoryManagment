using FactoryManagment.Domain.Entities;

namespace FactoryManagment.Application.Abstractions.Repositories;

public interface IUserRepository
{
    Task<ApplicationUser?> GetUserByEmailAsync(string email);
    Task<bool> CheckPasswordAsync(string userId, string password);
    Task<IEnumerable<string>> GetRolesAsync(string userId);
}
