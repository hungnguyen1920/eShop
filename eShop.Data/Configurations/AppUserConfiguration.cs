using eShop.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eShop.Data.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.Property(x => x.FirstName)
                   .HasMaxLength(200)
                   .IsRequired();
            builder.Property(x => x.LastName)
                   .HasMaxLength(200)
                   .IsRequired();
            builder.Property(x => x.DOB)
                   .IsRequired();
        }
    }
}