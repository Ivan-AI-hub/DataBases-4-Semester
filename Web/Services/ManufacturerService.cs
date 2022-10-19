using Microsoft.Extensions.Caching.Memory;
using WholesaleEntities.DataBaseControllers;
using WholesaleEntities.Models;

namespace Web.Services
{
    public class ManufacturerService: BaseTableService<Manufacturer>
    {
        public ManufacturerService(IMemoryCache cache, WholesaleContext context) : base(cache, context)
        {
        }

        protected override void Initialize(out List<Manufacturer> quary)
        {
            quary = Context.Manufacturers.ToList();
        }
    }
}
