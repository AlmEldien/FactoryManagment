using FactoryManagment.Application.Common.Contracts;
using FactoryManagment.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace FactoryManagment.Infrastructure.Persistence.Seeders;

internal static class SeedUser
{
    public static void SeedUserData(this ModelBuilder builder)
    {
        // Admin User
        var adminUser = new ApplicationUser
        {
            Id = DefaultUsers.AdminUserId,
            FirstName = DefaultUsers.AdminFirstName,
            LastName = DefaultUsers.AdminLastName,
            UserName = DefaultUsers.AdminUserName,
            NormalizedUserName = DefaultUsers.AdminUserName.ToUpper(),
            Email = DefaultUsers.AdminEmail,
            NormalizedEmail = DefaultUsers.AdminEmail.ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = DefaultUsers.AdminSecurityStamp,
            ConcurrencyStamp = DefaultUsers.AdminConcurrencyStamp,
            PasswordHash = DefaultUsers.AdminPasswordHash
        };

        // Regular User(Member)
        var regularUser = new ApplicationUser
        {
            Id = DefaultUsers.RegularUserId,
            FirstName = DefaultUsers.RegularUserFirstName,
            LastName = DefaultUsers.RegularUserLastName,
            UserName = DefaultUsers.RegularUserName,
            NormalizedUserName = DefaultUsers.RegularUserName.ToUpper(),
            Email = DefaultUsers.RegularUserEmail,
            NormalizedEmail = DefaultUsers.RegularUserEmail.ToUpper(),
            EmailConfirmed = true,
            SecurityStamp = DefaultUsers.RegularUserSecurityStamp,
            ConcurrencyStamp = DefaultUsers.RegularUserConcurrencyStamp,
            PasswordHash = DefaultUsers.RegularUserPasswordHash
        };

        builder.Entity<ApplicationUser>().HasData(adminUser, regularUser);
    }
}
