using System.Text;
using Web.Services;
using WholesaleEntities.Models;

namespace Web.Middleware
{
    public class ProductSearchForm1Middleware
    {
        private readonly RequestDelegate _next;

        public ProductSearchForm1Middleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context, ProductService productService, ManufacturerService manufacturerService)
        {

            await _next.Invoke(context);
            if (context.Request.Path == @"/Product/Search1")
            {
                
                var productName = GetValueFromCookie(context, "productName");
                var storageConditions = GetValueFromCookie(context, "storageConditions");
                var package = GetValueFromCookie(context, "package");
                var manufacturerId = int.Parse(GetValueFromCookie(context, "manufacturerName", "0"));

                IEnumerable<Product> products;
                if (manufacturerId == 0)
                {
                    products = productService.GetByCondition(x =>
                    {
                        return x.Name.Contains(productName) && x.StorageConditions.Contains(storageConditions) &&
                        x.Package.Contains(package);
                    });
                }
                else
                {
                    products = productService.GetByCondition(x =>
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

                foreach (var manufacturer in manufacturerService.GetAll())
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
                    builder.Append($"<td>Name</td><td>package</td><td>storageConditions</td><td>Manufacturer</td>");
                    foreach (var product in products)
                    {
                        builder.Append("<tr>");
                        builder.Append($"<td> {product.Name}</td><td> {product.Package}</td><td>{product.StorageConditions}</td><td> {product.Manufacturer.Name}</td>");
                        builder.Append("</tr>");
                    }
                    builder.Append("</table>");
                    builder.Append("</div>");
                }
                else
                {
                    builder.Append("<p><h2>products can not find</h2> <h1>-_-</h1></p>");
                }

                context.Response.Cookies.Append("productName", productName);
                context.Response.Cookies.Append("storageConditions", storageConditions);
                context.Response.Cookies.Append("package", package);
                context.Response.Cookies.Append("manufacturerName", manufacturerId.ToString());
                await context.Response.WriteAsync(builder.ToString());
            }
        }

        private string GetValueFromCookie(HttpContext context, string cookieName, string defaultValue = "")
        {
            if(context.Request.Query[cookieName].Count() > 0)
            {
                return context.Request.Query[cookieName][0];
            }
            else if (context.Request.Cookies[cookieName] != null)
            {
                return context.Request.Cookies[cookieName];
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
