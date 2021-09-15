using System.Collections.Generic;

namespace Models
{
    public class StoreFront
    {
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString(){
            return $"Store Name: {this.Name} Address : {this.Address}";
        }

        public List<Inventory> Inventories { get; set; }
    }
}