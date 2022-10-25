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
            return Context.Products.Include(x => x.Manufacturer).Include(x => x.Type).ToList();
        }

        public IEnumerable<Product> GetFromCach(int count, string cachKey)
        {
            IEnumerable<Product> products;
            if (!Cache.TryGetValue(cachKey, out products))
            {
                products = GetAll().Take(count);
                if (products != null)
                {
                    Cache.Set(cachKey, products, TimeSpan.FromSeconds(CacheTime));
                }
            }
            return products;
        }


        public IEnumerable<Product> GetByCondition(Func<Product, bool> predicate)
        {
            IEnumerable<Product> products = null;
            if (!Cache.TryGetValue(predicate.GetHashCode(), out products))
            {
                products = GetAll().Where(x => predicate(x));
                if (products != null)
                {
                    Cache.Set(predicate.GetHashCode(), products, TimeSpan.FromSeconds(CacheTime));
                }
            }
            return products;
        }
    }
}
