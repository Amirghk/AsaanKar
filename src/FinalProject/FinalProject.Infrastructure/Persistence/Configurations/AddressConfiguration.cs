using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class AddressConfiguration : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.Property(x => x.Content)
        .IsRequired()
        .HasMaxLength(2000);
        builder.Property(x => x.PostalCode)
        .IsRequired()
        .HasMaxLength(10);
        builder.Property(x => x.AddressCategory)
        .IsRequired();
        builder
            .HasOne(x => x.Customer)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder
            .HasOne(x => x.Expert)
            .WithOne(x => x.Address)
            .HasForeignKey<Address>(x => x.ExpertId)
            .OnDelete(DeleteBehavior.Cascade);

        builder
            .HasOne(x => x.City)
            .WithMany(x => x.Addresses)
            .HasForeignKey(x => x.CityId)
            .OnDelete(DeleteBehavior.NoAction);
        builder
            .HasMany(x => x.Orders)
            .WithOne(x => x.ReceiverAddress)
            .HasForeignKey(x => x.AddressId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}