using System;
using System.Collections.Generic;

namespace EntityTest
{
    public partial class ProvidersInformation
    {
        public string ProvidersName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public DateTime ReceipDate { get; set; }
        public double ReceipValue { get; set; }
    }
}
