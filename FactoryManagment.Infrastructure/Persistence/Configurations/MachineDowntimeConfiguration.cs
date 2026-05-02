using FactoryManagment.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryManagment.Infrastructure.Persistence.Configurations;

internal class MachineDowntimeConfiguration : IEntityTypeConfiguration<MachineDowntime>
{
    public void Configure(EntityTypeBuilder<MachineDowntime> builder)
    {
        builder.ToTable("MachineDowntimes");
        builder.HasKey(md => md.Id);

        // Enum are stored as string for readability in the DB
        builder.Property(md => md.Reason).HasConversion<string>().HasMaxLength(50);
        builder.Property(md => md.StartedAt).IsRequired().HasColumnType("datetime2");
        builder.Property(md => md.EndedAt).HasColumnType("datetime2");

        // Indexes:
        builder.HasIndex(md => new { md.MachineId, md.StartedAt });

        // Relationships:
        // ==============
        // MachineDowntime with Machine => one-to-many
        builder.HasOne(md => md.Machine)
            .WithMany(m => m.Downtimes)
            .HasForeignKey(md => md.MachineId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

