using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class ServiceExpertConfiguration : IEntityTypeConfiguration<ServiceExpert>
{
    public void Configure(EntityTypeBuilder<ServiceExpert> builder)
    {
        builder.HasKey(t => new { t.ServiceId, t.ExpertId });
        builder
            .HasOne(s => s.Service)
            .WithMany(e => e.ServiceExperts)
            .HasForeignKey(s => s.ServiceId);
        builder
            .HasOne(s => s.Expert)
            .WithMany(e => e.ServiceExperts)
            .HasForeignKey(s => s.ExpertId);
    }
}