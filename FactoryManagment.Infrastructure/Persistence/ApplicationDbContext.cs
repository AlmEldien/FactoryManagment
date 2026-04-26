using FactoryManagment.Domain.Entities;
using FactoryManagment.Domain.Enums;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FactoryManagment.Infrastructure.Persistence
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
    {
        public DbSet<Alert> Alerts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Alert>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.Title)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(a => a.Message)
                    .IsRequired()
                    .HasMaxLength(1000);

                entity.Property(a => a.Type)
                    .IsRequired()
                    .HasConversion<string>()
                    .HasMaxLength(50);

                entity.Property(a => a.CreatedAt)
                    .HasDefaultValueSql("GETUTCDATE()");

                entity.Property(a => a.IsRead)
                    .HasDefaultValue(false);

                entity.HasIndex(a => a.CreatedAt);
                entity.HasIndex(a => a.Type);
            });
        }
    }
}
