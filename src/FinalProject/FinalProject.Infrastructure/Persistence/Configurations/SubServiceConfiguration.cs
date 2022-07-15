using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class SubServiceConfiguration : IEntityTypeConfiguration<SubService>
{
    public void Configure(EntityTypeBuilder<SubService> builder)
    {
        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(4000);
        builder
            .HasOne(ss => ss.Service)
            .WithMany(s => s.SubServices)
            .HasForeignKey(ss => ss.ServiceId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}