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

        public IEnumerable<Manufacturer> GetByCondition(Func<Manufacturer, bool> predicate)
        {
            return Context.Manufacturers.Where(x => predicate(x)).AsEnumerable();
        }
    }
}
