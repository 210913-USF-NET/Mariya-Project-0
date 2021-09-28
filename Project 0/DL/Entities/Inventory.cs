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
        }

        public int InvenStoreId { get; set; }
        public int InvenProductId { get; set; }
        public int InventoryQuantity { get; set; }

        public virtual Product InvenProduct { get; set; }
        public virtual StoreFront InvenStore { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
