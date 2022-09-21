using System;
using System.Collections.Generic;

namespace EntityTest
{
    public partial class ReleaseReport
    {
        public int ReleaseReportId { get; set; }
        public DateTime ReciveDate { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Volume { get; set; }
        public int CustomerId { get; set; }
        public int EmployerId { get; set; }
        public int StorageId { get; set; }
        public int ProductId { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Employer Employer { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
        public virtual Storage Storage { get; set; } = null!;
    }
}
