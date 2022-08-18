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
using FinalProject.Endpoint.MiddleWare;
using StackExchange.Redis;
using FinalProject.Infrastructure.Persistence.SeedData;
using Microsoft.Extensions.FileProviders;
using FinalProject.Endpoint.Common.ErrorDescribers;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
//builder.Logging.AddSeq(builder.Configuration.GetSection("Seq"));
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

#region Caching

builder.Services.AddDistributedMemoryCache();


// TODO : get redis config from user secrets
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration["Redis:ConnectionString"];
    options.InstanceName = "";
});

builder.Services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect("localhost:6379,password=foobared"));

#endregion


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
}).AddEntityFrameworkStores<ApplicationDbContext>()
.AddErrorDescriber<FarsiIdentitiyErrorDescriber>();

/// <summary>
/// adds a global OperationCancelledException filter to handle the exception and short circuit the middleware
/// & a logging filter to log 4 stages of action execution
/// </summary>
builder.Services.AddControllersWithViews(options =>
{
    options.Filters.Add<OperationCancelledExceptionFilter>();
    options.Filters.Add<LogActionFilter>();
});

builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseErrorHandling();
    // app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    // app.UseHsts();
}
// app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "node_modules")),
    RequestPath = new PathString("/vendor")
});

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});
app.MapRazorPages();
app.Run();
