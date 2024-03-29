﻿using FinalProject.Application.Common.Interfaces;
using FinalProject.Application.Common.Interfaces.CacheRepositories;
using FinalProject.Application.Common.Interfaces.Repositories;
using FinalProject.Infrastructure.Persistence;
using FinalProject.Infrastructure.Repositories;
using FinalProject.Infrastructure.Services;
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
                options.UseSqlServer(configuration["SqlServer:ConnectionString"]));
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<ICityRepository, CityRepository>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IExpertRepository, ExpertRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IServiceRepository, ServiceRepository>();
            services.AddScoped<IUploadRepository, UploadRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped<ICategoryRepositoryCache, CategoryRepositoryCache>();
            services.AddScoped<ICityRepositoryCache, CityRepositoryCache>();
            services.AddScoped<IProvinceRepositoryCache, ProvinceRepositoryCache>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            return services;
        }
    }
}
