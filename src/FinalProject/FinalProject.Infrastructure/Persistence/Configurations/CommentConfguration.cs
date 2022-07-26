using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(2500);
        builder.Property(c => c.ImageId)
            .IsRequired(false);
        builder
            .HasOne(c => c.Customer)
            .WithMany(cu => cu.Comments)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasOne(c => c.Expert)
            .WithMany(e => e.Comments)
            .HasForeignKey(c => c.ExpertId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(c => c.Image)
            .WithOne(f => f.Comment)
            .HasForeignKey<Comment>(c => c.ImageId)
            .OnDelete(DeleteBehavior.SetNull);

    }
}
