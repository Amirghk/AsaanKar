using Microsoft.AspNetCore.Mvc.Filters;

namespace FinalProject.Endpoint.Common.Filters
{
    public class LogActionFilter : ActionFilterAttribute
    {
        private readonly ILogger<LogActionFilter> _logger;

        public LogActionFilter(ILogger<LogActionFilter> logger)
        {
            _logger = logger;
        }


        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Log(nameof(OnActionExecuting), context.RouteData);
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            Log(nameof(OnActionExecuted), context.RouteData);
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            Log(nameof(OnResultExecuting), context.RouteData);
        }
        public override void OnResultExecuted(ResultExecutedContext context)
        {
            Log(nameof(OnResultExecuted), context.RouteData);
        }


        private void Log(string methodName, RouteData routeData)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            _logger.LogTrace("{Method} Controller: {controller} Action: {action}", methodName, controllerName, actionName);
        }
    }
}
