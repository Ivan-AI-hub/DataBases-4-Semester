using Microsoft.Extensions.Caching.Memory;
using WholesaleEntities.DataBaseControllers;

namespace Web.Services
{
    public abstract class BaseTableService<T>
    {
        private IMemoryCache _cache;
        private WholesaleContext _context;
        private List<T> _quary;

        protected WholesaleContext Context => _context;
        protected IMemoryCache Cache => _cache;
        

        protected int CacheTime = 240;

        public BaseTableService(IMemoryCache cache, WholesaleContext context)
        {
            _cache = cache;
            _context = context;
            Initialize(out _quary);
        }

        protected abstract void Initialize(out List<T> quary);

        public List<T> GetAll()
        {
            return _quary.ToList();
        }
    }
}
