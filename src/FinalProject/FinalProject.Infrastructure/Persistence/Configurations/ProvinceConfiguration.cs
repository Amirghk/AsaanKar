using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class ProvinceConfiguration : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);
        builder
            .HasMany(p => p.Cities)
            .WithOne(c => c.Province)
            .HasForeignKey(c => c.ProvinceId);
    }
}