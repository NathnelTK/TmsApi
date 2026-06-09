using System.Diagnostics;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<RequestLoggingMiddleware> _logger;

    public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var correlationId = Guid.NewGuid().ToString("N")[..8];
        context.Response.Headers["X-Correlation-Id"] = correlationId;

        var method = context.Request.Method;
        var path = context.Request.Path;

        _logger.LogInformation("Request starting {Method} {Path} with CorrelationId {CorrelationId}", method, path, correlationId);

        var stopwatch = Stopwatch.StartNew();
        try
        {
            await _next(context);
        }
        finally
        {
            stopwatch.Stop();
            var elapsedMs = stopwatch.ElapsedMilliseconds;
            var statusCode = context.Response.StatusCode;

            _logger.LogInformation("Request finished {StatusCode} in {ElapsedMs}ms with CorrelationId {CorrelationId}", statusCode, elapsedMs, correlationId);
        }
    }
}
