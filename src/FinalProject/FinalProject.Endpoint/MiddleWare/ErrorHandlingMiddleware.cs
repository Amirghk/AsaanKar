using FinalProject.Application.Common.Exceptions;
using FinalProject.Domain.Enums;
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

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            HandleExceptionAsync(context, ex);
        }
    }

    private void HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError("Exception {Exception} Thrown At {Source}", exception, exception.Source);
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
