namespace Gorbilet.TestTask.Middlewares
{
    public class HeadersMiddleware
    {
        private readonly RequestDelegate _next;

        private const string headerName = "Header1";
        private const string headerValue = "123";

        public HeadersMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.Headers.TryAdd(headerName, headerValue);
            context.Response.Headers.TryAdd(headerName, headerValue);

            await _next.Invoke(context);
        }
    }
}
