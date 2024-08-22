using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Services.Middlewares;

public class SignatureValidationMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly ILogger<SignatureValidationMiddleware> _logger;

    public SignatureValidationMiddleware(RequestDelegate next, IConfiguration configuration, ILogger<SignatureValidationMiddleware> logger)
    {
        _next = next;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        // Retrieve signature from request header
        string signature = context.Request.Headers["Signature"];

        if (!string.IsNullOrEmpty(signature))
        {
            // Get the secret key from appsettings
            string secretKey = _configuration["AppSettings:SecretKey"];
            string requestPath = context.Request.Path;

            if ((requestPath.Contains("CreateNewStory", StringComparison.OrdinalIgnoreCase) ||
                 requestPath.Contains("UploadStoriesFromExcel", StringComparison.OrdinalIgnoreCase)) &&
                signature != _configuration["AppSettings:AdminToken"])
            {
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid signature");
            }
            
            // Check if the signature matches the secret key
            if (signature == secretKey || signature == _configuration["AppSettings:AdminToken"])
            {
                await _next(context); // Proceed to the next middleware
            }
            else
            {
                _logger.LogWarning("Invalid signature");
                context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                await context.Response.WriteAsync("Unauthorized: Invalid signature");
            }
        }
        else
        {
            _logger.LogWarning("Signature header missing");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Unauthorized: Signature header missing");
        }
    }
}

// Extension method used to add the middleware to the HTTP request pipeline
public static class SignatureValidationMiddlewareExtensions
{
    public static IApplicationBuilder UseSignatureValidationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<SignatureValidationMiddleware>();
    }
}