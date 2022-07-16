using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .HasOne(c => c.ProfilePicture)
            .WithOne(f => f.Customer)
            .HasForeignKey<FileDetail>(f => f.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasMany(c => c.Addresses)
            .WithOne(a => a.Customer)
            .HasForeignKey(a => a.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasMany(c => c.Comments)
            .WithOne(c => c.Customer)
            .HasForeignKey(c => c.CustomerId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
