using FinalProject.Application.Common.Exceptions;
using System.Net;
using System.Text.Json;

namespace FinalProject.Endpoint.MiddleWare;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnfinishedOrderException ex)
        {
            HandleExceptionAsync(context, ex);
        }
    }

    // TODO : Complete this
    private static void HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.Redirect("/Expert/Service/Error?type=unfinished");
    }
}

public static class ErrorHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
