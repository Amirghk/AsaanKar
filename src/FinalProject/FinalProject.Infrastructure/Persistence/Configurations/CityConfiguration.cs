using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class CityConfiguration : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);
        builder.HasOne(c => c.Province)
            .WithMany(p => p.Cities)
            .HasForeignKey(c => c.ProvinceId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasMany(x => x.Addresses)
            .WithOne(x => x.City)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
