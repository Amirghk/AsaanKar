namespace FinalProject.Application;

using FinalProject.Application.Common.Interfaces.Services;
using FinalProject.Application.Common.Services;
using Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAddressService, AddressService>();
        services.AddScoped<ICityService, CityService>();
        services.AddScoped<IProvinceService, ProvinceService>();
        services.AddScoped<IExpertService, ExpertService>();
        services.AddScoped<ICustomerService, CustomerService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IServiceService, ServiceService>();
        services.AddScoped<IUploadService, FileDetailService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IBidService, BidService>();
        return services;
    }
}