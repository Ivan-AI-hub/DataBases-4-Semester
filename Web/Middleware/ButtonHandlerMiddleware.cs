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
                var storageConditions = context.Request.Query["storageConditions"];
                var package = context.Request.Query["package"];
                var manufacturerId = context.Request.Query["manufacturerName"];
            }
            
            await _next.Invoke(context);
            
        }
    }
}
