namespace FarmManager.WebApi.Middlewares;

public class LoggerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LoggerMiddleware> _logger;

    public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var endpoint = context.GetEndpoint();
        var routeName = endpoint?.DisplayName ?? "Unknown";
        var method = context.Request.Method;
        var path = context.Request.Path;

        _logger.LogInformation("Calling Controller: {Route} - Method: {Method} - Path: {Path} - HTTP {HttpMethod}", routeName, method, path, method);

        await _next(context);

        var statusCode = context.Response.StatusCode;

        if (statusCode >= 200 && statusCode < 300)
        {
            _logger.LogInformation("Success - HTTP {StatusCode}", statusCode);
        }

        _logger.LogInformation("Request finished");
    }
}
