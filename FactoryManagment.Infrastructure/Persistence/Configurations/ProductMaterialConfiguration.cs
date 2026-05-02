using FactoryManagment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryManagment.Infrastructure.Persistence.Configurations;

internal class ProductMaterialConfiguration : IEntityTypeConfiguration<ProductMaterial>
{
    public void Configure(EntityTypeBuilder<ProductMaterial> builder)
    {
        builder.ToTable("ProductMaterials");

        // Composite PK — a material can only be assigned once per product
        builder.HasKey(pm => new { pm.ProductId, pm.MaterialId });

        builder.Property(pm => pm.QuantityUsed).IsRequired();
        builder.Property(pm => pm.AssignedAt).IsRequired().HasColumnType("datetime2");

        // Relationships:
        // ==============
        // ProductMaterial with Product => one-to-many
        builder.HasOne(pm => pm.Product)
            .WithMany(p => p.ProductMaterials)
            .HasForeignKey(pm => pm.ProductId)
            .OnDelete(DeleteBehavior.Restrict);
        // ProductMaterial with Material => one-to-many
        builder.HasOne(pm => pm.Material)
            .WithMany(p => p.ProductMaterials)
            .HasForeignKey(pm => pm.MaterialId)
            .OnDelete(DeleteBehavior.Restrict);

    }
}
