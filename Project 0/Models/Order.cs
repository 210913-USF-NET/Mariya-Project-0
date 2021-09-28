using System;
using System.Collections.Generic;

namespace Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerID { get; set; }
        public int StoreID { get; set; }
        public List<LineItem> LineItems { get; set; }
        public List<Product> Products { get; set; }
        public decimal Total { get; set; }
        public DateTime Date { get; set; }
    }
}