using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class FileDetailsConfiguration : IEntityTypeConfiguration<FileDetail>
{
    public void Configure(EntityTypeBuilder<FileDetail> builder)
    {
        builder
            .HasKey(f => f.Id);
        builder
            .Property(f => f.FileName)
            .IsRequired()
            .HasMaxLength(300);
    }
}