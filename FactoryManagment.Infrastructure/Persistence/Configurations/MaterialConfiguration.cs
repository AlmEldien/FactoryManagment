using FactoryManagment.Domain.Entities;
using FactoryManagment.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryManagment.Infrastructure.Persistence.Configurations;

internal class MaterialConfiguration : IEntityTypeConfiguration<Material>
{
    public void Configure(EntityTypeBuilder<Material> builder)
    {
        builder.ToTable("Materials");
        builder.HasKey(m => m.Id);

        // ValueObjects are stored as plain strings via HasConversion
        builder.Property(m => m.Name).IsRequired().HasConversion(name => name.Value, value => new EntityName(value)).HasMaxLength(200);
        builder.Property(m => m.Description).IsRequired().HasConversion(description => description.Value, value => new EntityDescription(value)).HasMaxLength(1000);
        builder.Property(m => m.Quantity).IsRequired();
        builder.Property(m => m.ConsumedQuantity).IsRequired();
        
        // Enum are stored as string for readability in the DB
        builder.Property(m => m.MaterialCondition).HasConversion<string>().HasMaxLength(50);
        builder.Property(m => m.EntryAt).IsRequired().HasColumnType("datetime2");

        // Indexes:
        // Unique index — material names must be distinct across the catalog
        builder.HasIndex(m => m.Name).IsUnique();
    }
}
