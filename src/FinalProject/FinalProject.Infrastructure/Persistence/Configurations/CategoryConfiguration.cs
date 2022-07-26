using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
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
            HasOne(x => x.ParentCategory)
            .WithMany(x => x.Categories)
            .HasForeignKey(x => x.ParentCategoryId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(s => s.Picture)
            .WithOne(f => f.Category)
            .HasForeignKey<Category>(f => f.PictureId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasMany(c => c.Services)
            .WithOne(s => s.Category)
            .HasForeignKey(s => s.CategoryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}