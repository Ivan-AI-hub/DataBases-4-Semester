using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using WholesaleEntities.DataBaseControllers;
using WholesaleEntities.Models;

namespace Web.Services
{
    public class ReceiptReportService : BaseTableService<ReceiptReport>
    {
        public ReceiptReportService(IMemoryCache cache, WholesaleContext context) : base(cache, context)
        {
        }

        protected override void Initialize(out List<ReceiptReport> quary)
        {
            quary = Context.ReceiptReports.Include(x => x.Provaider).Include(x => x.Product).Include(x => x.Employer).ToList();
        }
    }
}
