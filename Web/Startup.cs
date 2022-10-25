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
            services.AddSingleton<WholesaleContext>();
            services.AddTransient<ProductService>();
            services.AddTransient<ManufacturerService>();
            services.AddSession();
            services.AddMemoryCache();
            services.AddControllersWithViews();
        }

        public void Configure(IHostEnvironment environment, IApplicationBuilder app,
            ProductService productService)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }

            app.UseRouting();
            app.UseSession();
            app.UseMiddleware<ProductSearchForm1Middleware>();
            app.UseMiddleware<ProductSearchForm2Middleware>();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", (context) =>
                {
                    var builder = new StringBuilder();
                    builder.Append("<H1>Main page<H1>");
                    builder.Append(@"<a href = '/info'>Info about user</a></br>");
                    builder.Append(@"<a href = '/Product'>Products table</a></br>");
                    builder.Append(@"<a href = '/Product/Search1'>Product search with cookie</a></br>");
                    builder.Append(@"<a href = '/Product/Search2'>Product search with session</a></br>");
                    return context.Response.WriteAsync(builder.ToString());
                });
                

                #region ProductTable
                endpoints.MapGet("/Product", (context) =>
                {
                    var builder = new StringBuilder();
                    builder.Append("<div>");
                    builder.Append("<H1>Products table<H1>");
                    builder.Append("<table>");
                    builder.Append($"<td>Name</td><td>package</td><td>storageConditions</td><td>Manufacturer</td>");
                    foreach (var product in productService.GetFromCach(20, "allProducts"))
                    {
                        builder.Append("<tr>");
                        builder.Append($"<td> {product.Name}</td><td> {product.Package}</td><td> {product.StorageConditions}</td><td> {product.Manufacturer.Name}</td>");
                        builder.Append("</tr>");
                    }
                    builder.Append("</table>");
                    builder.Append("</div>");
                    return context.Response.WriteAsync(builder.ToString());
                });
                #endregion

                endpoints.MapGet("/info", (context) =>
                {
                    string browser = context.Request.Headers["sec-ch-ua"];
                    string platform = context.Request.Headers["sec-ch-ua-platform"];

                    return context.Response.WriteAsync("<p>Browser: " + browser + "</p><p>Platform: " + platform);
                });
                endpoints.MapControllers();
            });
        }
    }
}

