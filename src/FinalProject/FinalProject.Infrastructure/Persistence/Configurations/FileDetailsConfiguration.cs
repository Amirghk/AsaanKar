using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class FileDetailsConfiguration : IEntityTypeConfiguration<FileDetail>
{
    public void Configure(EntityTypeBuilder<FileDetail> builder)
    {
        builder
            .Property(f => f.FileName)
            .IsRequired()
            .HasMaxLength(300);
        builder
            .HasOne(f => f.Customer)
            .WithOne(c => c.ProfilePicture)
            .HasForeignKey<FileDetail>(f => f.CustomerId);
        builder
            .HasOne(f => f.Expert)
            .WithMany(e => e.Pictures)
            .HasForeignKey(f => f.ExpertId);
        builder
            .HasOne(f => f.Comment)
            .WithOne(c => c.Image)
            .HasForeignKey<Comment>(f => f.ImageId);
        builder
            .HasOne(f => f.Service)
            .WithOne(s => s.FileDetails)
            .HasForeignKey<FileDetail>(f => f.ServiceId);
    }
}