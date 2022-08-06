using FinalProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinalProject.Infrastructure.Persistence.SeedData;

public static class ModelBuilderExtensions
{
    // TODO : find out how to get ids of added elements and how to access usermanager
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

    }
}