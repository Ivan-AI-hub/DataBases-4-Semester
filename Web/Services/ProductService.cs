using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WholesaleEntities.DataBaseControllers;
using WholesaleEntities.Models;

namespace Web.Services
{
    public class ProductService : ITableService<Product>
    {
        public WholesaleContext Context { get; }

        public IMemoryCache Cache { get; }

        public int CacheTime { get; }

        public ProductService(IMemoryCache cache, WholesaleContext context)
        {
            Cache = cache;
            Context = context;
            CacheTime = 240;
        }

        public IEnumerable<Product> GetAll()
        {
            return Context.Products.Include(x => x.Manufacturer).Include(x => x.Type).AsEnumerable();
        }

        public IEnumerable<Product> GetByCondition(Func<Product, bool> predicate)
        {
            return Context.Products.Include(x => x.Manufacturer).Include(x => x.Type).Where(x => predicate(x)).AsEnumerable();
        }
    }
}
