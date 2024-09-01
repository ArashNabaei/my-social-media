namespace my_social_media.Middlewares
{
    public class LoggingMiddleware : IMiddleware
    {
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(ILogger<LoggingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {

            _logger.LogInformation("Handling request: {Method} {Path}", context.Request.Method, context.Request.Path);

            await next(context);

            _logger.LogInformation("Finished handling request: {Method} {Path}", context.Request.Method, context.Request.Path);

        }
    }
}
