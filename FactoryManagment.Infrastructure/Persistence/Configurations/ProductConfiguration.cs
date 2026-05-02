using FactoryManagment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryManagment.Infrastructure.Persistence.Configurations;

internal class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");
        builder.HasKey(p => p.Id);

        // ValueObjects are stored as plain strings via HasConversion
        builder.Property(p => p.Name).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Description).IsRequired().HasMaxLength(1000);

        // Enum are stored as string for readability in the DB
        builder.Property(p => p.Status).HasConversion<string>().HasMaxLength(50);
        builder.Property(p => p.ProductQuality).HasConversion<string>().HasMaxLength(50);
        builder.Property(p => p.StartedProducingAt).IsRequired().HasColumnType("datetime2");
        builder.Property(p => p.FinishedAt).HasColumnType("datetime2");
        builder.Property(p => p.AssignedToMachineAt).HasColumnType("datetime2");

        // Indexes:
        builder.HasIndex(p => p.Name);
        // Filtered index — only indexes rows where the product is assigned to a machine
        builder.HasIndex(p => new { p.MachineId, p.AssignedToMachineAt }).HasFilter("[AssignedToMachineAt] IS NOT NULL");

        // Relationships:
        // ==============
        // Product with Machine => one-to-many
        builder.HasOne(p => p.Machine)
            .WithMany(m => m.Products)
            .HasForeignKey(p => p.MachineId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
