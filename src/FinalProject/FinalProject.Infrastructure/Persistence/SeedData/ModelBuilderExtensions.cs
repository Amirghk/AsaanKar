using FinalProject.Domain.Entities;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.Infrastructure.Persistence.SeedData;

public static class ModelBuilderExtensions
{
    // TODO : find out how to access usermanager
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
        //ApplicationUser user = new ApplicationUser()
        //{
        //    Id = "b74ddd14-6340-4840-95c2-db12554843e5",
        //    UserName = "Admin",
        //    Email = "admin@gmail.com",
        //    LockoutEnabled = false,
        //    PhoneNumber = "1234567890"
        //};
        //PasswordHasher<ApplicationUser> passwordHasher = new PasswordHasher<ApplicationUser>();
        //passwordHasher.HashPassword(user, "Asdasdasd");

    }
}