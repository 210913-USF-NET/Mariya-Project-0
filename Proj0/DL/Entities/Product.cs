using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Product
    {
        public Product()
        {
            Inventories = new HashSet<Inventory>();
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public string ProductGenere { get; set; }
        public string ProductDescription { get; set; }

        public virtual ICollection<Inventory> Inventories { get; set; }
    }
}
