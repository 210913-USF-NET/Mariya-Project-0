namespace Models
{
    public class LineItem
    {
     
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int StoreId { get; set; }
        public Product Item { get; set; }
        public int Quantity { get; set; }
    }
}