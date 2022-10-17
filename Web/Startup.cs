using Microsoft.EntityFrameworkCore;
using System.Text;
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
            services.AddTransient<ReceiptReportService>();
            services.AddMemoryCache();
            services.AddControllersWithViews();
        }

        public void Configure(IHostEnvironment environment, IApplicationBuilder app,
            CustomerService customerService, ProductService productService, ReceiptReportService receiptReportService)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseRouting();
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
                endpoints.MapGet("/info", (context) =>
                {
                    return context.Response.WriteAsync($"<H1>User: {context.User.ToString()}<H1>");
                });
                endpoints.MapControllers();
            });
        }
    }
}

