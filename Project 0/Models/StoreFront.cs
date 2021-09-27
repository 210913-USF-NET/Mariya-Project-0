using System.Collections.Generic;

namespace Models
{
    public class StoreFront
    {
        public int StoreID { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString(){
            return $"\nStore ID: {this.StoreID}\nStore Name: {this.Name}\nAddress: {this.Address}";
        }

        public List<Inventory> Inventories { get; set; }
    }
}