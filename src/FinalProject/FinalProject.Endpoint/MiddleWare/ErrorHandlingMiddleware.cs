using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Enums;
using FinalProject.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using System.Net;
using System.Text.Json;

namespace FinalProject.Endpoint.MiddleWare;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            HandleExceptionAsync(context, ex, userManager);
        }
    }

    private void HandleExceptionAsync(HttpContext context, Exception exception, UserManager<ApplicationUser> userManager)
    {
        _logger.LogInformation("Exception occured for {User}", userManager.GetUserId(context.User));

        switch (exception)
        {
            case NotFoundException:
                _logger.LogWarning("Exception {Exception} Thrown At {Source}", exception, exception.Source);
                break;
            case UnfinishedOrderException:
                _logger.LogWarning("Exception {Exception} Thrown At {Source}", exception, exception.Source);
                break;
            case SqlException:
                _logger.LogCritical("Exception {Exception} Thrown At {Source}", exception, exception.Source);
                break;
            default:
                _logger.LogError("Exception {Exception} Thrown At {Source}", exception, exception.Source);
                break;
        }
        ErrorTypes error = exception switch
        {
            NotFoundException => ErrorTypes.NotFound,
            UnfinishedOrderException => ErrorTypes.UnfinishedOrder,
            _ => ErrorTypes.UnDefined
        };

        context.Response.Redirect($"/Home/Error?type={error}");
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
