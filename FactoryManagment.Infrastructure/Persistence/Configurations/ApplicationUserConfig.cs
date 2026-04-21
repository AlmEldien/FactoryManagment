using FactoryManagment.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FactoryManagment.Infrastructure.Persistence.Configurations;

public class ApplicationUserConfig : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);
        builder.Ignore(x => x.FullName);

        builder.OwnsMany(x => x.RefreshTokens, tb =>
        {
            tb.ToTable("RefreshTokens");
            tb.WithOwner().HasForeignKey("UserId");
            tb.HasKey("UserId", "Token");
            tb.Property(r => r.Token).HasMaxLength(256);
        });
    }
}
