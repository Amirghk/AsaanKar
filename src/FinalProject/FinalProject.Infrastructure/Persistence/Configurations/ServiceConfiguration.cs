using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(4000);
        builder
            .HasOne(ss => ss.Category)
            .WithMany(s => s.Services)
            .HasForeignKey(ss => ss.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(s => s.Orders)
            .WithOne(o => o.Service)
            .HasForeignKey(o => o.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}