using System;
using System.Collections.Generic;

namespace EntityTest
{
    public partial class ReceiptReport
    {
        public int ReceiptReportId { get; set; }
        public DateTime ReciveDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public double Volume { get; set; }
        public int ProvaiderId { get; set; }
        public int EmployerId { get; set; }
        public int StorageId { get; set; }
        public int ProductId { get; set; }

        public virtual Employer Employer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Provaider Provaider { get; set; } = null!;
        public virtual Storage Storage { get; set; } = null!;
    }
}
