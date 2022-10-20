using System.Text;
using Web.Services;
using WholesaleEntities.Models;

namespace Web.Middleware
{
    public class ButtonHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private ProductService _productService;
        private ManufacturerService _manufacturerService;

        public ButtonHandlerMiddleware(RequestDelegate next, ProductService productService, ManufacturerService manufacturerService)
        {
            this._next = next;
            _productService = productService;
            _manufacturerService = manufacturerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            await _next.Invoke(context);
            if (context.Request.Path == @"/Product/Search")
            {
                var productName = context.Request.Query["productName"].Count() > 0 ? context.Request.Query["productName"][0] : "";
                var storageConditions = context.Request.Query["storageConditions"].Count() > 0? context.Request.Query["storageConditions"][0] : "";
                var package = context.Request.Query["package"].Count() > 0 ? context.Request.Query["package"][0] : "";
                var manufacturerId = context.Request.Query["manufacturerName"].Count() > 0? int.Parse(context.Request.Query["manufacturerName"][0]) : 0;

                IEnumerable<Product> products;
                if (manufacturerId == 0)
                {
                    products = _productService.GetByCondition(x =>
                    {
                        return x.Name.Contains(productName) && x.StorageConditions.Contains(storageConditions) &&
                        x.Package.Contains(package);
                    });
                }
                else
                {
                    products = _productService.GetByCondition(x =>
                    {
                        return x.Name.Contains(productName) && x.StorageConditions.Contains(storageConditions) &&
                        x.Package.Contains(package) && x.ManufacturerId == manufacturerId;
                    });
                }

                var builder = new StringBuilder();
                builder.Append("<div>");
                builder.Append("<H1>Products Search<H1>");
                builder.Append("<form method='get'>");

                builder.Append("<p><b>Product name</b></p>");
                builder.Append($"<input type = 'text' name = 'productName' value = '{productName}'></input>");

                builder.Append("<p><b>Storage conditions</b></p>");
                builder.Append($"<input type = 'text' name = 'storageConditions'  value = '{storageConditions}'></input>");

                builder.Append("<p><b>Package</b></p>");
                builder.Append($"<input type = 'text' name = 'package'  value = '{package}'></input>");

                builder.Append("<p><b>Manufacturer</b></p>");
                builder.Append($"<select name='manufacturerName'  value = '{manufacturerId}'>");
                builder.Append($"<option value='0'> Any</opinion>");

                foreach (var manufacturer in _manufacturerService.GetAll())
                {
                    if(manufacturerId == manufacturer.ManufacturerId)
                    {
                        builder.Append($"<option selected value='{manufacturer.ManufacturerId}'> {manufacturer.Name}</opinion>");
                    }
                    else
                        builder.Append($"<option value='{manufacturer.ManufacturerId}'> {manufacturer.Name}</opinion>");
                }

                builder.Append("</select>");

                builder.Append("<input type='submit' name='submit' value='Submit' />");
                builder.Append("</form>");
                builder.Append("</div>");

                if (products.Count() != 0)
                {
                    builder.Append("<div>");
                    builder.Append("<H1>Products table<H1>");
                    builder.Append("<table>");
                    builder.Append($"<td>Name</td><td>package</td><td>Manufacturer</td>");
                    foreach (var product in products)
                    {
                        builder.Append("<tr>");
                        builder.Append($"<td> {product.Name}</td><td> {product.Package}</td><td> {product.Manufacturer.Name}</td>");
                        builder.Append("</tr>");
                    }
                    builder.Append("</table>");
                    builder.Append("</div>");
                }
                else
                {
                    builder.Append("<p><h2>products can not find</h2> <h1>-_-</h1></p>");
                }
                await context.Response.WriteAsync(builder.ToString());
            }
        }
    }
}
