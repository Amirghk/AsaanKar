using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder
            .HasKey(c => c.Id);
        builder
            .Property(c => c.Content)
            .IsRequired()
            .HasMaxLength(2500);
        builder
            .HasOne(c => c.Customer)
            .WithMany(cu => cu.Comments)
            .HasForeignKey(c => c.CustomerId);
        builder
            .HasOne(c => c.Expert)
            .WithMany(e => e.Comments)
            .HasForeignKey(c => c.ExpertId);
        builder
            .HasOne(c => c.Image)
            .WithOne(f => f.Comment)
            .HasForeignKey<Comment>(c => c.ImageId);
    }
}
