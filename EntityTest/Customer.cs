using System;
using System.Collections.Generic;

namespace EntityTest
{
    public partial class Customer
    {
        public Customer()
        {
            ReleaseReports = new HashSet<ReleaseReport>();
        }

        public int CustomerId { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string TelephoneNumber { get; set; } = null!;

        public virtual ICollection<ReleaseReport> ReleaseReports { get; set; }
    }
}
