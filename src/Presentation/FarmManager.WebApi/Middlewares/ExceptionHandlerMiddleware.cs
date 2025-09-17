using FarmManager.Application.Exceptions;

namespace FarmManager.WebApi.Middlewares;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotFoundException ex)
        {
            _logger.LogWarning("NotFoundException: {Message} - TraceId: {TraceId}", ex.Message, context.TraceIdentifier);
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            context.Response.ContentType = "application/json";
            var error = new { Error = ex.Message, TraceId = context.TraceIdentifier };
            await context.Response.WriteAsJsonAsync(error);
        }
        catch (DuplicateResourceException ex) 
        {
            _logger.LogWarning("DuplicateResourceException: {Message} - TraceId: {TraceId}", ex.Message, context.TraceIdentifier);
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Response.ContentType = "application/json";
            var error = new { Error = ex.Message, TraceId = context.TraceIdentifier };
            await context.Response.WriteAsJsonAsync(error);
        }
        catch (Exception ex)
        {
            _logger.LogError("Unhandled Exception: {ExceptionType} - {Message} - TraceId: {TraceId}", ex.GetType().Name, ex.Message, context.TraceIdentifier);
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";
            var error = new { Error = "An internal server error occurred.", TraceId = context.TraceIdentifier };
            await context.Response.WriteAsJsonAsync(error);
        }
    }
}