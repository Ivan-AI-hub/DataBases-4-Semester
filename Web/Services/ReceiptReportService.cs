using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WholesaleEntities.DataBaseControllers;
using WholesaleEntities.Models;

namespace Web.Services
{
    public class ReceiptReportService : ITableService<ReceiptReport>
    {
        public WholesaleContext Context { get; }

        public IMemoryCache Cache { get; }

        public int CacheTime { get; }

        public ReceiptReportService(IMemoryCache cache, WholesaleContext context)
        {
            Cache = cache;
            Context = context;
            CacheTime = 240;
        }

        public IEnumerable<ReceiptReport> GetAll()
        {
            return Context.ReceiptReports
                .Include(x => x.Provaider)
                .Include(x => x.Product)
                .Include(x => x.Employer)
                .Include(x => x.Storage).AsEnumerable();
        }

        public IEnumerable<ReceiptReport> GetByCondition(Func<ReceiptReport, bool> predicate)
        {
            return Context.ReceiptReports
                .Include(x => x.Provaider)
                .Include(x => x.Product)
                .Include(x => x.Employer)
                .Include(x => x.Storage).Where(x => predicate(x)).AsEnumerable();
        }
    }
}
