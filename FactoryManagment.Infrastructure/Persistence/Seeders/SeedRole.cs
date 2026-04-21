using FactoryManagment.Application.Common.Contracts;
using FactoryManagment.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FactoryManagment.Infrastructure.Persistence.Seeders;

internal static class SeedRole
{
    public static void SeedRoleData(this ModelBuilder builder)
    {
        // Seed default roles
        builder.Entity<ApplicationRole>().HasData(
            new ApplicationRole
            {
                Id = DefaultRoles.AdminRoleId,
                Name = DefaultRoles.Admin,
                NormalizedName = DefaultRoles.Admin.ToUpper(),
                ConcurrencyStamp = DefaultRoles.AdminRoleConcurrencyStamp,
                IsDefault = false,
                IsDeleted = false
            },
            new ApplicationRole
            {
                Id = DefaultRoles.MemberRoleId,
                Name = DefaultRoles.Member,
                NormalizedName = DefaultRoles.Member.ToUpper(),
                ConcurrencyStamp = DefaultRoles.MemberRoleConcurrencyStamp,
                IsDefault = true,
                IsDeleted = false
            }
        );

        // Seed user-role relationships
        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string>
            {
                RoleId = DefaultRoles.AdminRoleId,
                UserId = DefaultUsers.AdminUserId
            },
            new IdentityUserRole<string>
            {
                RoleId = DefaultRoles.MemberRoleId,
                UserId = DefaultUsers.RegularUserId
            }
        );
    }
}
