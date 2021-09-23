using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Inventory
    {
        public Inventory()
        {
            LineItems = new HashSet<LineItem>();
            Orders = new HashSet<Order>();
        }

        public int InventoryId { get; set; }
        public int InvenStoreId { get; set; }
        public int InvenProductId { get; set; }
        public int ProductInventory { get; set; }

        public virtual Product InvenProduct { get; set; }
        public virtual StoreFront InvenStore { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
