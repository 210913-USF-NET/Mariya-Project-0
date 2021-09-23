using System.Collections.Generic;

namespace Models
{
    public class Inventory
    {
        public int InventoryID { get; set; }        
        public int StoreID { get; set; }
        public Product Item { get; set; }
        public int Quantity { get; set; }
    }
}