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

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);
builder.Services.AddAutoMapper(typeof(VMMappingProfile).Assembly);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddAuthorization(options =>
        options.AddPolicy(
        "IsAdmin", policyBuilder => policyBuilder
                    .RequireClaim("IsAdmin")));

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

app.MapRazorPages();

app.Run();
