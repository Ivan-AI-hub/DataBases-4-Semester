using System;
using System.Collections.Generic;

namespace WholesaleEntities.Models
{
    public partial class Storage
    {
        public Storage()
        {
            ReceiptReports = new HashSet<ReceiptReport>();
            ReleaseReports = new HashSet<ReleaseReport>();
        }

        public int StorageId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ReceiptReport> ReceiptReports { get; set; }
        public virtual ICollection<ReleaseReport> ReleaseReports { get; set; }
    }
}
