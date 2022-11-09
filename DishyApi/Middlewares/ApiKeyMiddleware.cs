using DishyApi.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace DishyApi.Middlewares;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IOptions<ApiKeySettings> apiKeySettings)
    {
        string apiKey = apiKeySettings.Value.Key;

        if (!context.Request.Headers.TryGetValue("x-api-key", out StringValues values) && values.Count != 1)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API KEY not found.");

            return;
        }

        var requestKey = values[0];

        if (requestKey != apiKey)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("API KEY is invalid.");

            return;
        }

        await _next(context);
    }
}