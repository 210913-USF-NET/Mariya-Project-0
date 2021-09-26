using System;
using System.Collections.Generic;

namespace Models
{
    public class Customer
    {
        public Customer(){
            this.CustomerDefaultStoreID = 100;
        }

        public Customer(string name) : this ()
        {
            this.Name = name;
            this.CustomerDefaultStoreID = 100;
        }

        public Customer(string name, string address) : this(name){
            this.Address = address;
            this.CustomerDefaultStoreID = 100;
        }
        public Customer(string name, int age, string address, int storeId) : this(name, address){
            this.CustomerDefaultStoreID = storeId;
        }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int CustomerDefaultStoreID { get; set; }
        public List<Order> Orders { get; set; }

        public override string ToString()
        {
            return $"Customer Id: {this.CustomerId}, Name: {this.Name}, UserName: {this.UserName}, Email: {this.Email}, Address: {this.Address}, Preferred Store: {this.CustomerDefaultStoreID}";
        }
    }
}
