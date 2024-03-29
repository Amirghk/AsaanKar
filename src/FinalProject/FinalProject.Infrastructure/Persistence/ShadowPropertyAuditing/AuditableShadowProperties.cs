using System.Security.Claims;
using System.Security.Principal;
using FinalProject.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace FinalProject.Infrastructure.Persistence;

public static class AuditableShadowProperties
{
    public static readonly Func<object, string> EfPropertyCreatedByBrowserName =
        entity => EF.Property<string>(entity, CreatedByBrowserName!);

    public static readonly string CreatedByBrowserName = nameof(CreatedByBrowserName);

    public static readonly Func<object, string> EfPropertyModifiedByBrowserName =
        entity => EF.Property<string>(entity, ModifiedByBrowserName!);

    public static readonly string ModifiedByBrowserName = nameof(ModifiedByBrowserName);

    public static readonly Func<object, string> EfPropertyCreatedByIp =
        entity => EF.Property<string>(entity, CreatedByIp!);

    public static readonly string CreatedByIp = nameof(CreatedByIp);

    public static readonly Func<object, string> EfPropertyModifiedByIp =
        entity => EF.Property<string>(entity, ModifiedByIp!);

    public static readonly string ModifiedByIp = nameof(ModifiedByIp);

    public static readonly Func<object, int?> EfPropertyCreatedByUserId =
        entity => EF.Property<int?>(entity, CreatedByUserId!);

    public static readonly string CreatedByUserId = nameof(CreatedByUserId);

    public static readonly Func<object, int?> EfPropertyModifiedByUserId =
        entity => EF.Property<int?>(entity, ModifiedByUserId!);

    public static readonly string ModifiedByUserId = nameof(ModifiedByUserId);

    public static readonly Func<object, DateTimeOffset?> EfPropertyCreatedDateTime =
        entity => EF.Property<DateTimeOffset?>(entity, CreatedDateTime!);

    public static readonly string CreatedDateTime = nameof(CreatedDateTime);

    public static readonly Func<object, DateTimeOffset?> EfPropertyModifiedDateTime =
        entity => EF.Property<DateTimeOffset?>(entity, ModifiedDateTime!);

    public static readonly string ModifiedDateTime = nameof(ModifiedDateTime);


    public static void AddAuditableShadowProperties(this ModelBuilder modelBuilder)
    {
        foreach (var entityType in modelBuilder.Model
                     .GetEntityTypes()
                     .Where(e => typeof(IAuditableEntity).IsAssignableFrom(e.ClrType)))
        {
            modelBuilder.Entity(entityType.ClrType)
                .Property<string>(CreatedByBrowserName).HasMaxLength(1000);
            modelBuilder.Entity(entityType.ClrType)
                .Property<string>(ModifiedByBrowserName).HasMaxLength(1000);

            modelBuilder.Entity(entityType.ClrType)
                .Property<string>(CreatedByIp).HasMaxLength(255);
            modelBuilder.Entity(entityType.ClrType)
                .Property<string>(ModifiedByIp).HasMaxLength(255);

            modelBuilder.Entity(entityType.ClrType)
                .Property<string?>(CreatedByUserId);
            modelBuilder.Entity(entityType.ClrType)
                .Property<string?>(ModifiedByUserId);

            modelBuilder.Entity(entityType.ClrType)
                .Property<DateTimeOffset?>(CreatedDateTime);
            modelBuilder.Entity(entityType.ClrType)
                .Property<DateTimeOffset?>(ModifiedDateTime);
        }
    }

    public static void SetAuditableEntityPropertyValues(
        this ChangeTracker changeTracker,
        IHttpContextAccessor httpContextAccessor)
    {
        HttpContext? httpContext = httpContextAccessor?.HttpContext;
        var userAgent = httpContext?.Request?.Headers["User-Agent"].ToString();
        var userIp = httpContext?.Connection?.RemoteIpAddress?.ToString();
        var now = DateTimeOffset.UtcNow;
        string? userId = GetUserId(httpContext);

        var modifiedEntries = changeTracker.Entries<IAuditableEntity>()
            .Where(x => x.State == EntityState.Modified);
        foreach (var modifiedEntry in modifiedEntries)
        {
            modifiedEntry.Property(ModifiedDateTime).CurrentValue = now;
            modifiedEntry.Property(ModifiedByBrowserName).CurrentValue = userAgent;
            modifiedEntry.Property(ModifiedByIp).CurrentValue = userIp;
            modifiedEntry.Property(ModifiedByUserId).CurrentValue = userId;
        }

        var addedEntries = changeTracker.Entries<IAuditableEntity>()
            .Where(x => x.State == EntityState.Added);
        foreach (var addedEntry in addedEntries)
        {
            addedEntry.Property(CreatedDateTime).CurrentValue = now;
            addedEntry.Property(CreatedByBrowserName).CurrentValue = userAgent;
            addedEntry.Property(CreatedByIp).CurrentValue = userIp;
            addedEntry.Property(CreatedByUserId).CurrentValue = userId;
        }
    }

    private static string? GetUserId(HttpContext httpContext)
    {
        string? userId = null;
        var userIdValue = httpContext?.User?.Identity?.GetUserId();
        if (!string.IsNullOrWhiteSpace(userIdValue)) userId = userIdValue;
        return userId;
    }

    public static string? GetUserId(this IIdentity identity)
    {
        return identity?.GetUserClaimValue(ClaimTypes.NameIdentifier);
    }

    public static string? GetUserClaimValue(this IIdentity identity, string claimType)
    {
        var identity1 = identity as ClaimsIdentity;
        return identity1?.FindFirstValue(claimType);
    }

    public static string? FindFirstValue(this ClaimsIdentity identity, string claimType)
    {
        return identity?.FindFirst(claimType)?.Value;
    }
}