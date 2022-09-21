using System;
using System.Collections.Generic;

namespace EntityTest
{
    public partial class ProductType
    {
        public ProductType()
        {
            Products = new HashSet<Product>();
        }

        public int ProductTypeId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string Feature { get; set; } = null!;

        public virtual ICollection<Product> Products { get; set; }
    }
}
