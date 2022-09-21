using System;
using System.Collections.Generic;

namespace EntityTest
{
    public partial class ProductBalance
    {
        public string ProductName { get; set; } = null!;
        public string Storage { get; set; } = null!;
        public double ReceipValue { get; set; }
        public double? ReleaseValue { get; set; }
        public double Balance { get; set; }
    }
}
