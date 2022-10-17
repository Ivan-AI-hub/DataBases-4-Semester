using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WholesaleEntities.DataBaseControllers;
using WholesaleEntities.Models;

namespace Web.Services
{
    public class ProductService : BaseTableService<Product>
    {
        public ProductService(IMemoryCache cache, WholesaleContext context) : base(cache, context)
        {
        }

        protected override void Initialize(out List<Product> quary)
        {
            quary = Context.Products.Include(x => x.Manufacturer).Include(x => x.Type).ToList(); 
        }
    }
}
