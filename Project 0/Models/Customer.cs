using System;

namespace Models
{
    public class Customer
    {
        public Customer(){}

        public Customer(string name) : this ()
        {
            this.Name = name;
        }

        public Customer( string name, int age) : this (name)
        {
            this.Age = age;
        }

        public Customer(string name, int age, string city) : this(name, age){
            this.City = city;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string City { get; set; }

        public List<Order> Orders { get; set; }
    }
}
