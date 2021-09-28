using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class LineItem
    {
        public int LineOrderId { get; set; }
        public int LineStoreId { get; set; }
        public int LineInvenProdId { get; set; }
        public int? OrderProductQantity { get; set; }

        public virtual Inventory Line { get; set; }
        public virtual Order LineOrder { get; set; }
    }
}
