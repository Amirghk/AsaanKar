using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class UploadConfiguration : IEntityTypeConfiguration<Upload>
{
    public void Configure(EntityTypeBuilder<Upload> builder)
    {
        builder
            .Property(f => f.FileName)
            .IsRequired()
            .HasMaxLength(300);
        builder
            .HasOne(f => f.Customer)
            .WithOne(c => c.ProfilePicture)
            .HasForeignKey<Customer>(c => c.ProfilePictureId)
            .OnDelete(DeleteBehavior.SetNull);
        builder
            .HasOne(f => f.Expert)
            .WithMany(e => e.WorkSamples)
            .HasForeignKey(f => f.ExpertId);
        builder
            .HasOne(f => f.Comment)
            .WithOne(c => c.Image)
            .HasForeignKey<Comment>(f => f.ImageId);
        builder
            .HasOne(f => f.Category)
            .WithOne(s => s.Picture)
            .HasForeignKey<Category>(f => f.PictureId);
    }
}