using Microsoft.EntityFrameworkCore;
using System.Text;
using Web.Middleware;
using Web.Services;
using WholesaleEntities.DataBaseControllers;

namespace Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<WholesaleContext>();
            services.AddTransient<CustomerService>();
            services.AddTransient<ProductService>();
            services.AddTransient<ManufacturerService>();
            services.AddTransient<ReceiptReportService>();
            services.AddMemoryCache();
            services.AddControllersWithViews();
        }

        public void Configure(IHostEnvironment environment, IApplicationBuilder app,
            CustomerService customerService, ProductService productService, ReceiptReportService receiptReportService,
            ManufacturerService manufacturerService)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseRouting();
            app.UseMiddleware<ButtonHandlerMiddleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", (context) =>
                {
                    var builder = new StringBuilder();
                    builder.Append("<H1>Main page<H1>");
                    builder.Append(@"<a href = '/info'>Info about user</a></br>");
                    builder.Append(@"<a href = '/Customer'>Customers table</a></br>");
                    builder.Append(@"<a href = '/ReceiptReport'>ReceiptReports table</a></br>");
                    builder.Append(@"<a href = '/Product'>Products table</a></br>");
                    return context.Response.WriteAsync(builder.ToString());
                });
                #region ReceiptReportTable
                endpoints.MapGet("/ReceiptReport", (context) =>
                {
                    var builder = new StringBuilder();
                    builder.Append("<div>");
                    builder.Append("<H1>ReceiptReports table<H1>");
                    builder.Append("<table>");
                    builder.Append($"<td> OrderDate</td>" +
                                       $"<td> Product</td>" +
                                       $"<td> Volume</td>" +
                                       $"<td> Storage</td>" +
                                       $"<td> Provaider</td>");
                    foreach (var receive in receiptReportService.GetAll())
                    {
                        builder.Append("<tr>");
                        builder.Append($"<td> {receive.OrderDate}</td>" +
                                       $"<td> {receive.Product.Name}</td>" +
                                       $"<td> {receive.Volume}</td>" +
                                       $"<td> {receive.Storage.Name}</td>" +
                                       $"<td> {receive.Provaider.Name}</td>");
                        builder.Append("</tr>");
                    }
                    builder.Append("</table>");
                    builder.Append("</div>");
                    return context.Response.WriteAsync(builder.ToString());
                });
                #endregion

                #region ProductTable
                endpoints.MapGet("/Product", (context) =>
                {
                    var builder = new StringBuilder();
                    builder.Append("<div>");
                    builder.Append("<H1>Products table<H1>");
                    builder.Append("<table>");
                    builder.Append($"<td>Name</td><td>Type</td><td>Manufacturer</td>");
                    foreach (var product in productService.GetAll())
                    {
                        builder.Append("<tr>");
                        builder.Append($"<td> {product.Name}</td><td> {product.Type.Name}</td><td> {product.Manufacturer.Name}</td>");
                        builder.Append("</tr>");
                    }
                    builder.Append("</table>");
                    builder.Append("</div>");
                    return context.Response.WriteAsync(builder.ToString());
                });
                endpoints.MapGet("/Product/Search", (context) =>
                {
                    var builder = new StringBuilder();
                    builder.Append("<div>");
                    builder.Append("<H1>Products Search<H1>");
                    builder.Append("<form>");

                    builder.Append("<p><b>Product name</b></p>");
                    builder.Append("<input type = 'text' name = 'productName'></input>");

                    builder.Append("<p><b>Storage conditions</b></p>");
                    builder.Append("<input type = 'text' name = 'storageConditions'></input>");

                    builder.Append("<p><b>Package</b></p>");
                    builder.Append("<input type = 'text' name = 'package'></input>");

                    builder.Append("<p><b>Manufacturer</b></p>");
                    builder.Append("<select name='manufacturerName'>");
                    foreach (var manufacturer in manufacturerService.GetAll())
                    {
                        builder.Append($"<option value='{manufacturer.ManufacturerId}'> {manufacturer.Name}</opinion>");
                    }

                    builder.Append("</select>");

                    builder.Append("<button name = 'button'>Search</button>");
                    builder.Append("</form>");
                    builder.Append("</div>");
                    return context.Response.WriteAsync(builder.ToString());
                });
                #endregion

                #region CustomerTable
                endpoints.MapGet("/Customer", (context) =>
                {
                    var builder = new StringBuilder();
                    builder.Append("<div>");
                    builder.Append("<H1>Customers table<H1>");
                    builder.Append("<table>");
                    builder.Append($"<td>Name</td><td>Address</td><td>TelephoneNumber</td>");
                    foreach (var customer in customerService.GetAll())
                    {
                        builder.Append("<tr>");
                        builder.Append($"<td> {customer.Name}</td><td> {customer.Address}</td><td> {customer.TelephoneNumber}</td>");
                        builder.Append("</tr>");
                    }
                    builder.Append("</table>");
                    builder.Append("</div>");
                    return context.Response.WriteAsync(builder.ToString());
                });
                #endregion
                endpoints.MapGet("/info", (context) =>
                {
                    return context.Response.WriteAsync($"<H1>User: {context.User.ToString()}<H1>");
                });
                endpoints.MapControllers();
            });
        }
    }
}

