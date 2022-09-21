using System;
using System.Collections.Generic;

namespace EntityTest
{
    public partial class Employer
    {
        public Employer()
        {
            ReceiptReports = new HashSet<ReceiptReport>();
            ReleaseReports = new HashSet<ReleaseReport>();
        }

        public int EmployerId { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<ReceiptReport> ReceiptReports { get; set; }
        public virtual ICollection<ReleaseReport> ReleaseReports { get; set; }
    }
}
