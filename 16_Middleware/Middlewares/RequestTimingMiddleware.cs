using System.Diagnostics;

namespace _16_Middleware.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestTimingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // requestin basladigi zamani al
            var watch = Stopwatch.StartNew();

            await _next(context);

            watch.Stop();

            var elapsed = watch.ElapsedMilliseconds; // Geçen süreyi ms cinsinden al

            Debug.WriteLine($"Request [{context.Request.Method}] {context.Request.Path} İşlem süresi {elapsed} ms.");
        }
    }
}
