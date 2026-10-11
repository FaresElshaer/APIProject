using System.Diagnostics;

namespace APIProject.Middlewares.cs
{
    public class profilingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<profilingMiddleware> _logger;
        public profilingMiddleware(RequestDelegate next, ILogger<profilingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _next(context);
            stopwatch.Stop();
            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
            _logger.LogInformation($"Request [{context.Request.Method}] {context.Request.Path} took {stopwatch.ElapsedMilliseconds} ms");
        }
    }
}
