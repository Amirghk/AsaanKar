using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinalProject.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder
            .HasKey(f => f.Id);
        builder
            .Property(x => x.Description)
            .IsRequired(false)
            .HasMaxLength(4000);
        builder
            .HasOne(o => o.ReceiverAddress)
            .WithMany(a => a.Orders)
            .HasForeignKey(o => o.AddressId);
        builder
            .HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);
        builder
            .HasOne(o => o.Expert)
            .WithMany(e => e.Orders)
            .HasForeignKey(o => o.ExpertId);
        builder
            .HasOne(o => o.Service)
            .WithMany(s => s.Orders)
            .HasForeignKey(o => o.ServiceId);
    }
}