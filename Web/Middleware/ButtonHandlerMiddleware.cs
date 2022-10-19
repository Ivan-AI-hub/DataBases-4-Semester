namespace Web.Middleware
{
    public class ButtonHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ButtonHandlerMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path == @"/Product/Search")
            {
                var productName = context.Request.Query["productName"];
            }
            
            await _next.Invoke(context);
            
        }
    }
}
