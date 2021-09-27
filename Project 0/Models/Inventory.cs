using System.Collections.Generic;

namespace Models
{
    public class Inventory
    {
       // public string  StoreName { get; set; }        
        public int StoreID { get; set; }
       // public string StoreName { get; set; }
        public int ProductID { get; set; }
        // public string Product { get; set; }
        public Product Item { get; set; }
        public int Quantity { get; set; }

        


        //public List<LineItem>  { get; set; }
        public override string ToString()
        {
            return $"\nProduct ID : {this.ProductID} Quantity available: {this.Quantity}";
        }
    }
}