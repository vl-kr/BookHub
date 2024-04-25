using System.Net;

namespace WebAPI.Middlewares;

public class TokenAuthenticationMiddleware
{
    private readonly string? _hardcodedToken;
    private readonly ILogger<TokenAuthenticationMiddleware> _logger;
    private readonly RequestDelegate _next;

    public TokenAuthenticationMiddleware(
        RequestDelegate next,
        IConfiguration configuration,
        ILogger<TokenAuthenticationMiddleware> logger
    )
    {
        _next = next;
        _hardcodedToken = configuration["Authentication:AuthToken"];
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        string? token = context.Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(token) && token.Equals(_hardcodedToken))
        {
            await _next(context);
        }
        else
        {
            _logger.LogWarning("Unauthorized access attempted.");
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("Unauthorized");
        }
    }
}
