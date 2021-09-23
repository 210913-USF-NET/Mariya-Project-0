namespace Models
{
    public class LineItem
    {
        public int MyProperty { get; set; }
        public int OrderID { get; set; }
        public Product Item { get; set; }
        public int Quantity { get; set; }
    }
}