using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class ExpertConfiguration : IEntityTypeConfiguration<Expert>
{
    public void Configure(EntityTypeBuilder<Expert> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .ValueGeneratedNever();
        builder
            .Property(x => x.Bio)
            .HasMaxLength(4000);
        builder
            .HasMany(e => e.Services)
            .WithMany(s => s.Experts)
            .UsingEntity<ServiceExpert>(
            x => x
            .HasOne(se => se.Service)
            .WithMany(e => e.ServiceExperts)
            .HasForeignKey(se => se.ServiceId),
            x => x
            .HasOne(se => se.Expert)
            .WithMany(e => e.ServiceExperts)
            .HasForeignKey(se => se.ExpertId)
            );
        builder
            .HasMany(e => e.Comments)
            .WithOne(c => c.Expert)
            .HasForeignKey(c => c.ExpertId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(e => e.Orders)
            .WithOne(o => o.Expert)
            .HasForeignKey(o => o.ExpertId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
