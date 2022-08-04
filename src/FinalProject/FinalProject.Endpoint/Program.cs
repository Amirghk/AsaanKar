using AutoMapper;
using FinalProject.Application;
using FinalProject.Infrastructure.Mappings;
using FinalProject.Endpoint.Common.Mappings;
using FinalProject.Infrastructure;
using FinalProject.Infrastructure.Identity;
using FinalProject.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Configuration;
using Serilog;
using Serilog.Extensions.Logging;
using FinalProject.Application.Common.ConfigurationModels;
using FinalProject.Endpoint.Common.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSeq(builder.Configuration.GetSection("Seq"));
builder.Logging.AddProvider(new SerilogLoggerProvider());

//builder.Host.UseSerilog((hostingContext, provider, loggerConfiguration) =>
//    loggerConfiguration.ReadFrom.Configuration(builder.Configuration)
//    );
builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .ReadFrom.Configuration(ctx.Configuration));

// bind appsettings json to Appsettings class
builder.Services.Configure<AppSettings>(builder.Configuration);
builder.Services.AddSingleton(builder.Configuration.Get<AppSettings>());


builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(VMMappingProfile).Assembly);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy(
            "IsAdmin",
            policyBuilder => policyBuilder.RequireClaim("IsAdmin"));
    options.AddPolicy(
        "IsCustomer",
        policyBuilder => policyBuilder.RequireClaim("IsCustomer"));
    options.AddPolicy(
        "IsExpert",
        policyBuilder => policyBuilder.RequireClaim("IsExpert"));
}
);

// Add services to the container.
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    // TODO : REMOVE LATER
    options.SignIn.RequireConfirmedAccount = false;
    options.Lockout.AllowedForNewUsers = false;
    options.Password.RequiredLength = 7;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<ApplicationDbContext>();

/// <summary>
/// adds a global OperationCancelledException filter to handle the exception and short circuit the middleware
/// </summary>
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<OperationCancelledExceptionFilter>();
});

builder.Services.AddRazorPages();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();
app.Run();
