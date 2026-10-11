namespace APIProject.Middlewares.cs
{
    public class RateLimitingMiddlewares
    {
        private readonly RequestDelegate _next;
        static int counter = 0;
        private static DateTime _lastRequestDate = DateTime.Now;
        public RateLimitingMiddlewares(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            var currentTime = DateTime.Now;
            if ((currentTime - _lastRequestDate).TotalSeconds > 1)
            {
                counter = 0;
                _lastRequestDate = currentTime;
            }
            counter++;
            if (counter > 10)
            {
                context.Response.StatusCode = 429; // Too Many Requests
                await context.Response.WriteAsync("Rate limit exceeded. Try again later.");
                return;
            }
            await _next(context);
        }
    }
}