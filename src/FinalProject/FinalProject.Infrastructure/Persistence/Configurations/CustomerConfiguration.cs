using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .HasKey(x => x.Id);
        builder
            .HasOne(c => c.ProfilePicture)
            .WithOne(f => f.Customer)
            .HasForeignKey<FileDetail>(f => f.CustomerId);
    }
}
