using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class SubServiceConfiguration : IEntityTypeConfiguration<SubService>
{
    public void Configure(EntityTypeBuilder<SubService> builder)
    {
        builder.HasKey(x => x.Id);
        builder
            .Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(4000);
    }
}