using Microsoft.Extensions.Caching.Memory;
using WholesaleEntities.DataBaseControllers;
using WholesaleEntities.Models;

namespace Web.Services
{
    public class ManufacturerService: ITableService<Manufacturer>
    {
        public WholesaleContext Context { get; }

        public IMemoryCache Cache { get; }

        public int CacheTime { get; }

        public ManufacturerService(IMemoryCache cache, WholesaleContext context)
        {
            Cache = cache;
            Context = context;
            CacheTime = 240;
        }

        public IEnumerable<Manufacturer> GetAll()
        {
            return Context.Manufacturers.AsEnumerable();
        }


        public IEnumerable<Manufacturer> GetFromCach(int count, string cachKey)
        {
            IEnumerable<Manufacturer> manufacturers;
            if (!Cache.TryGetValue(cachKey, out manufacturers))
            {
                manufacturers = GetAll().Take(count);
                if (manufacturers != null)
                {
                    Cache.Set(cachKey, manufacturers, TimeSpan.FromSeconds(CacheTime));
                }
            }
            return manufacturers;
        }

        public IEnumerable<Manufacturer> GetByCondition(Func<Manufacturer, bool> predicate)
        {
            IEnumerable<Manufacturer> manufacturers = null;
            if (!Cache.TryGetValue(predicate.GetHashCode(), out manufacturers))
            {
                manufacturers = GetAll().Where(x => predicate(x));
                if (manufacturers != null)
                {
                    Cache.Set(predicate.GetHashCode(), manufacturers, TimeSpan.FromSeconds(CacheTime));
                }
            }
            return manufacturers;
        }
    }
}
