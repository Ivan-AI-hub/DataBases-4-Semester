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
            IEnumerable<Customer> customers = null;
            if (!Cache.TryGetValue(predicate.GetHashCode(), out customers))
            {
                customers = GetAll().Where(x => predicate(x));
                if (customers != null)
                {
                    Cache.Set(predicate.GetHashCode(), customers, TimeSpan.FromSeconds(CacheTime));
                }
            }
            return customers;
        }

        public IEnumerable<Customer> GetAll(WholesaleContext Context)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Customer> GetByCondition(WholesaleContext Context, Func<Customer, bool> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
