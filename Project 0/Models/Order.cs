namespace Models
{
    public class Order
    {
        public List<LineItems> LineItems { get; set; }
        public decimal Total { get; set; }
        public date Date { get; set; }
    }
}