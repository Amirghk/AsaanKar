using FinalProject.Infrastructure.Identity;
using FinalProject.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Persistence;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlServer(@"Server=(LocalDb)\MSSQLLOCALDB;Database=FinalProject;Trusted_Connection=True;");
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfiguration(new AddressConfiguration());
        builder.ApplyConfiguration(new CityConfiguration());
        builder.ApplyConfiguration(new CustomerConfiguration());
        builder.ApplyConfiguration(new ExpertConfiguration());
        builder.ApplyConfiguration(new CommentConfiguration());
        builder.ApplyConfiguration(new FileDetailsConfiguration());
        builder.ApplyConfiguration(new OrderConfiguration());
        builder.ApplyConfiguration(new ProvinceConfiguration());
        builder.ApplyConfiguration(new ServiceConfiguration());
        builder.ApplyConfiguration(new ServiceExpertConfiguration());
        builder.ApplyConfiguration(new SubServiceConfiguration());
    }
}