using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder
            .Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(256);
        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(5000);
        builder.
            HasOne(x => x.ParentService)
            .WithMany(x => x.Services)
            .HasForeignKey(x => x.ParentServiceId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(s => s.FileDetails)
            .WithOne(f => f.Service)
            .HasForeignKey<FileDetail>(f => f.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(s => s.SubServices)
            .WithOne(ss => ss.Service)
            .HasForeignKey(ss => ss.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(s => s.Orders)
            .WithOne(o => o.Service)
            .HasForeignKey(o => o.ServiceId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}