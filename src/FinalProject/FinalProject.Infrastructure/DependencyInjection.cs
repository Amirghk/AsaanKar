﻿using FinalProject.Domain.Interfaces;
using FinalProject.Infrastructure.Persistence;
using FinalProject.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FinalProject.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
            this IServiceCollection services,
            ConfigurationManager configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IExpertRepository, ExpertRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<ISubServiceRepository, SubServiceRepository>();
            services.AddScoped<IFileDetailRepository, FileDetailRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            return services;
        }
    }
}