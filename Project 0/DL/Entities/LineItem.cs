using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class LineItem
    {
        public int OrderId1 { get; set; }
        public int OrderInvenId { get; set; }
        public int? OrderProductQantity { get; set; }

        public virtual Order OrderId1Navigation { get; set; }
        public virtual Inventory OrderInven { get; set; }
    }
}
