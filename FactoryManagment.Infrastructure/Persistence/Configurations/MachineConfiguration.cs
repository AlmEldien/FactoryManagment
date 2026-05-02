using FactoryManagment.Domain.Entities;
using FactoryManagment.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryManagment.Infrastructure.Persistence.Configurations;

internal class MachineConfiguration : IEntityTypeConfiguration<Machine>
{
    public void Configure(EntityTypeBuilder<Machine> builder)
    {
        builder.ToTable("Machines");
        builder.HasKey(m => m.Id);

        // ValueObjects are stored as plain strings via HasConversion
        builder.Property(m => m.Name).IsRequired().HasConversion(name => name.Value, value => new EntityName(value)).HasMaxLength(200);
        builder.Property(m => m.Description).IsRequired().HasConversion(description => description.Value, value => new EntityDescription(value)).HasMaxLength(1000);
        builder.Property(m => m.MaxCapacityPerHour).IsRequired();
        builder.Property(m => m.ShiftHours).IsRequired();
    }
}
