using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Persistence.SeedData;

public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Province>().HasData(
            new Province
            {
                Id = 1,
                Name = "Tehran",
                IsSupported = true
            }
        );

        modelBuilder.Entity<City>().HasData(
           new City
           {
               Id = 1,
               Name = "Tehran",
               IsSupported = true,
               ProvinceId = 1,
           }
);
        ApplicationUser user = new ApplicationUser()
        {
            Id = "d74ddd24-6340-4840-95c2-db12554843e5",
            UserName = "adminss@gmail.com",
            Email = "adminss@gmail.com",
            LockoutEnabled = false,
            PhoneNumber = "1234567890",
            NormalizedEmail = "ADMINSS@GMAIL.COM",
            NormalizedUserName = "ADMINSS@GMAIL.COM",
        };
        PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        user.PasswordHash = passwordHasher.HashPassword(user, "Asdasdasd");

        modelBuilder.Entity<ApplicationUser>().HasData(user);
        modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
            new IdentityUserClaim<string>
            {
                ClaimType = "IsAdmin",
                ClaimValue = true.ToString(),
                Id = 52,
                UserId = user.Id
            }
            );

    }
}