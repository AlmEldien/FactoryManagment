using FactoryManagment.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FactoryManagment.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Product>         Products         { get; set; }
        public DbSet<Material>        Materials        { get; set; }
        public DbSet<Machine>         Machines         { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<MachineDowntime> MachineDowntimes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
