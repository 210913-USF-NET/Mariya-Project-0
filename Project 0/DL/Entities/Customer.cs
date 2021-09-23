using System;
using System.Collections.Generic;

#nullable disable

namespace DL.Entities
{
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerUserName { get; set; }
        public string CustomerPassWord { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public int CustomerStore { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
