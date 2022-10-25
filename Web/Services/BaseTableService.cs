using Microsoft.Extensions.Caching.Memory;
using WholesaleEntities.DataBaseControllers;

namespace Web.Services
{
    public interface ITableService<T>
    {
        public WholesaleContext Context { get; }
        public IMemoryCache Cache { get; }

        public int CacheTime { get; }
        public IEnumerable<T> GetAll();
        public IEnumerable<T> GetFromCach(int count, string cachKey);
        public IEnumerable<T> GetByCondition(Func<T, bool> predicate);
    }
}
