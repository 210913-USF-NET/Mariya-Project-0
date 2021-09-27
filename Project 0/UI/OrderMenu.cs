using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using StoreBL;

namespace UI
{
    
    public class OrderMenu : IMenuCust
    {
        private IBL _bl;
  
        public OrderMenu (IBL bl)
        {
            _bl = bl;
        }
        
         public void Start(Customer shopper)
        {
            bool exit = false;
            string input = "";
            do
            {
                Main:
                Console.WriteLine("[1] View Inventory");
                Console.WriteLine("[2] Start a new Order");
                Console.WriteLine("[x] Go to Main Menu");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ViewInventory(shopper);
                        break;
                    case "2":
                        StartOrder(shopper);
                        break;
                        //case "3":
                        //Checkout(shopper);
                        //break;
                    case "x":
                        Console.WriteLine("Have a great day!");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (!exit);

        }

        private void StartOrder(Customer cust)
        {
            int prodQt;
            int ItemID;
            //a way to temp store prod and qt while shopping
            
            ViewInventory(cust);// to see inventory?
            System.Console.WriteLine("Please enter the Product ID you want:");
            ItemID = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("Please enter the Product Quantity you want:");
            prodQt = Convert.ToInt32(Console.ReadLine());
            // ShoppingCart.MyCart.Add();
            // myCart.Add(prod)
        }

        private void ViewInventory(Customer cust)
        { 
            List<Inventory> storeInven = _bl.GetInventoryByStoreID(cust);
            List<Product> myInventory = _bl.ProductsList();
            StoreFront myStore = _bl.GetMyStore(cust);
            Console.WriteLine("**********************************************************");
            Console.WriteLine($"You are shopping at:{myStore}\n");
            Console.WriteLine("**********************************************************");

            var tempInventory = from m1 in storeInven
                join m2 in myInventory on m1.ProductID equals m2.ProductId
                select new {m1.ProductID, m2.Name, m1.Quantity, m2.Price,m2.Genre, m2.Description};

                foreach (var item in tempInventory)
            {
                
                System.Console.WriteLine($"Product Quantity Available: {item.Quantity}\nProduct Name: {item.Name}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
                Console.WriteLine("**********************************************************");
            }
        }
    }
}