using System;
using System.Collections.Generic;

namespace EntityTest
{
    public partial class Product
    {
        public Product()
        {
            ReceiptReports = new HashSet<ReceiptReport>();
            ReleaseReports = new HashSet<ReleaseReport>();
        }

        public int ProductId { get; set; }
        public string Name { get; set; } = null!;
        public int ManufacturerId { get; set; }
        public int TypeId { get; set; }
        public string StorageConditions { get; set; } = null!;
        public string? Package { get; set; }
        public DateTime StorageLife { get; set; }

        public virtual Manufacturer Manufacturer { get; set; } = null!;
        public virtual ProductType Type { get; set; } = null!;
        public virtual ICollection<ReceiptReport> ReceiptReports { get; set; }
        public virtual ICollection<ReleaseReport> ReleaseReports { get; set; }
    }
}
