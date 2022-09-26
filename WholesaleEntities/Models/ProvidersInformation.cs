using System;
using System.Collections.Generic;

namespace WholesaleEntities.Models
{
    public partial class ProvidersInformation
    {
        public string ProvidersName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public DateTime ReceipDate { get; set; }
        public double ReceipValue { get; set; }
    }
}
