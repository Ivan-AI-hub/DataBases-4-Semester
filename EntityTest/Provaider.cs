using System;
using System.Collections.Generic;

namespace EntityTest
{
    public partial class Provaider
    {
        public Provaider()
        {
            ReceiptReports = new HashSet<ReceiptReport>();
        }

        public int ProvaiderId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;

        public virtual ICollection<ReceiptReport> ReceiptReports { get; set; }
    }
}
