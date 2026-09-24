namespace Recipes.Api.Middleware;

public class ExceptionHandingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandingMiddleware> _logger;


    public ExceptionHandingMiddleware(RequestDelegate next, ILogger<ExceptionHandingMiddleware> logger)
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
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = 500,
                message = ex.Message
            });
        }
    }
}