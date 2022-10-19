using Microsoft.Extensions.Caching.Memory;
using WholesaleEntities.DataBaseControllers;
using WholesaleEntities.Models;

namespace Web.Services
{
    public class CustomerService : ITableService<Customer>
    {
        public WholesaleContext Context { get; }

        public IMemoryCache Cache { get; }

        public int CacheTime { get; }

        public CustomerService(IMemoryCache cache, WholesaleContext context)
        {
            Cache = cache;
            Context = context;
            CacheTime = 240;
        }

        public IEnumerable<Customer> GetAll()
        {
            return Context.Customers.AsEnumerable();
        }

        public IEnumerable<Customer> GetByCondition(Func<Customer, bool> predicate)
        {
            return Context.Customers.Where(x => predicate(x)).AsEnumerable();
        }
    }
}
