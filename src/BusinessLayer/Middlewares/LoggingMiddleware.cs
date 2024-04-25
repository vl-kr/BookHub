using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Middlewares;

public class LoggingMiddleware
{
    private readonly ILogger<LoggingMiddleware> _logger;
    private readonly RequestDelegate _next;
    private readonly string _source;

    public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger, string source)
    {
        _next = next;
        _logger = logger;
        _source = source;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation(
            $"{_source} {DateTime.Now:yyyy-MM-dd HH:mm:ss} Received {context.Request.Method} request at {context.Request.Path} from {context.Connection.RemoteIpAddress}"
        );

        await _next(context);
    }
}
