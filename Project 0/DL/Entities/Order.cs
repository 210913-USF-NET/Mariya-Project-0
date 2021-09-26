using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Order
    {
        public Order()
        {
            LineItems = new HashSet<LineItem>();
        }

        public int OrderId { get; set; }
        public int OrderAccountId { get; set; }
        public int OrderStoreId { get; set; }
        public decimal OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }

        public virtual Customer OrderNavigation { get; set; }
        public virtual ICollection<LineItem> LineItems { get; set; }
    }
}
