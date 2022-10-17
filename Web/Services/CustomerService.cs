using Microsoft.Extensions.Caching.Memory;
using WholesaleEntities.DataBaseControllers;
using WholesaleEntities.Models;

namespace Web.Services
{
    public class CustomerService : BaseTableService<Customer>
    {
        public CustomerService(IMemoryCache cache, WholesaleContext context) : base(cache, context)
        {
        }

        protected override void Initialize(out List<Customer> quary)
        {
            quary = Context.Customers.ToList();
        }
    }
}
