using FactoryManagment.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FactoryManagment.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Product>         Products         { get; set; }
        public DbSet<Material>        Materials        { get; set; }
        public DbSet<Machine>         Machines         { get; set; }
        public DbSet<ProductMaterial> ProductMaterials { get; set; }
        public DbSet<MachineDowntime> MachineDowntimes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Auto-discovers and applies all IEntityTypeConfiguration classes in this assembly
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }
}
