using Microsoft.AspNetCore.Identity;
namespace FactoryManagment.Domain.Entities;

public class ApplicationRole : IdentityRole
{
    public bool IsDefault { get; set; }
    public bool IsDeleted { get; set; }
}
