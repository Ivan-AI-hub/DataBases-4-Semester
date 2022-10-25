using System.Text;
using Web.Models;
using Web.Services;
using WholesaleEntities.Models;

namespace Web.Middleware
{
    public class ProductSearchForm2Middleware
    {
        private readonly RequestDelegate _next;
        private ProductService _productService;
        private ManufacturerService _manufacturerService;

        public ProductSearchForm2Middleware(RequestDelegate next, ProductService productService, ManufacturerService manufacturerService)
        {
            this._next = next;
            _productService = productService;
            _manufacturerService = manufacturerService;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            await _next.Invoke(context);
            if (context.Request.Path == @"/Product/Search2")
            {
                var model = GetModelFromSession(context);

                IEnumerable<Product> products;
                if (model.ManufacturerId == 0)
                {
                    products = _productService.GetByCondition(x =>
                    {
                        return x.Name.Contains(model.ProductName) && x.StorageConditions.Contains(model.StorageConditions) &&
                        x.Package.Contains(model.Package);
                    });
                }
                else
                {
                    products = _productService.GetByCondition(x =>
                    {
                        return x.Name.Contains(model.ProductName) && x.StorageConditions.Contains(model.StorageConditions) &&
                        x.Package.Contains(model.Package) && x.ManufacturerId == model.ManufacturerId;
                    });
                }

                var builder = new StringBuilder();
                builder.Append("<div>");
                builder.Append("<H1>Products Search<H1>");
                builder.Append("<form method='get'>");

                builder.Append("<p><b>Product name</b></p>");
                builder.Append($"<input type = 'text' name = 'productName' value = '{model.ProductName}'></input>");

                builder.Append("<p><b>Storage conditions</b></p>");
                builder.Append($"<input type = 'text' name = 'storageConditions'  value = '{model.StorageConditions}'></input>");

                builder.Append("<p><b>Package</b></p>");
                builder.Append($"<input type = 'text' name = 'package'  value = '{model.Package}'></input>");

                builder.Append("<p><b>Manufacturer</b></p>");
                builder.Append($"<select name='manufacturerName'  value = '{model.ManufacturerId}'>");
                builder.Append($"<option value='0'> Any</opinion>");

                foreach (var manufacturer in _manufacturerService.GetAll())
                {
                    if (model.ManufacturerId == manufacturer.ManufacturerId)
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
                        builder.Append($"<td> {product.Name}</td><td> {product.Package}</td><td> {product.StorageConditions}</td><td> {product.Manufacturer.Name}</td>");
                        builder.Append("</tr>");
                    }
                    builder.Append("</table>");
                    builder.Append("</div>");
                }
                else
                {
                    builder.Append("<p><h2>products can not find</h2> <h1>-_-</h1></p>");
                }

                context.Session.Set("model", model);
                await context.Response.WriteAsync(builder.ToString());
            }
        }

        private ProductSearchModel GetModelFromSession(HttpContext context)
        {
            if (context.Request.Query["productName"].Count() > 0)
            {
                ProductSearchModel model = new ProductSearchModel
                {
                    ProductName = context.Request.Query["productName"],
                    StorageConditions = context.Request.Query["storageConditions"],
                    Package = context.Request.Query["package"],
                    ManufacturerId = int.Parse(context.Request.Query["manufacturerName"])
                };
                context.Session.Set("model", model);
                return model;
            }
            else if (context.Session.Keys.Contains("model"))
            {
                ProductSearchModel model = context.Session.Get<ProductSearchModel>("model");
                return model;
            }
            else
            {
                ProductSearchModel model = new ProductSearchModel { 
                    ProductName = "",
                    StorageConditions = "",
                    Package = "",
                    ManufacturerId = 0
                };
                context.Session.Set("model", model);
                return model;
            }
        }
    }
}
