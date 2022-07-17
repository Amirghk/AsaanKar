using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Identity;
using FinalProject.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FinalProject.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Address> Addresses { get; set; } = null!;
    public DbSet<City> Cities { get; set; } = null!;
    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<Customer> Customers { get; set; } = null!;
    public DbSet<Expert> Experts { get; set; } = null!;
    public DbSet<Upload> FileDetails { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<Province> Provinces { get; set; } = null!;
    public DbSet<ServiceExpert> ServiceExperts { get; set; } = null!;
    public DbSet<Category> Services { get; set; } = null!;
    public DbSet<Service> SubServices { get; set; } = null!;
    public DbSet<Bid> Suggestions { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddAuditableShadowProperties();


        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AddressConfiguration());
        builder.ApplyConfiguration(new CityConfiguration());
        builder.ApplyConfiguration(new CustomerConfiguration());
        builder.ApplyConfiguration(new ExpertConfiguration());
        builder.ApplyConfiguration(new CommentConfiguration());
        builder.ApplyConfiguration(new UploadConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new ProvinceConfiguration());
        builder.ApplyConfiguration(new CategoryConfiguration());
        builder.ApplyConfiguration(new ServiceExpertConfiguration());
        builder.ApplyConfiguration(new ServiceConfiguration());
    }

    public override int SaveChanges()
    {
        ChangeTracker.DetectChanges();
        SetShadowProperties();
        var result = base.SaveChanges();
        ChangeTracker.AutoDetectChangesEnabled = true;
        return result;
    }

    private void SetShadowProperties()
    {
        var httpContextAccessor = this.GetService<IHttpContextAccessor>();
        ChangeTracker.SetAuditableEntityPropertyValues(httpContextAccessor);
    }
}