using System;
using System.Collections.Generic;
using Models;
using StoreBL;
using System.Linq;

namespace UI
{
    public class CustomerNavigation  : IMenuCust
    {
        private IBL _bl;
        
        public CustomerNavigation(IBL bl)
        {
            _bl = bl;
           
        }
        
        public void Start(Customer shopper)
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("[1] Start a new cart");
                Console.WriteLine("[2] View shopping history");
                Console.WriteLine("[3] Change your default store");
                Console.WriteLine("[x] Exit");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        MenuFactory.GetMenuCust("order menu").Start(shopper);
                        break;
                    case "2":
                        ViewOrderHistory(shopper);
                        break;
                        case "3":
                        shopper = ChangeCustomerStore(shopper);
                        break;
                    case "x":
                        Console.WriteLine("Thank you");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (!exit);
        }

        private Customer ChangeCustomerStore(Customer cust)
        {
            List <StoreFront> chooseStore = _bl.GetStoreFronts();
            System.Console.WriteLine($"Your current StoreID is: {cust.CustomerDefaultStoreID}");
            System.Console.WriteLine("List of stores available:");
            foreach (var item in chooseStore)
            {
                System.Console.WriteLine(item);
            }
            Console.WriteLine("Enter your preferred StoreID:");
            int store = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine($"You entered store ID: {store}\nYour Profile has been updated. ");
            cust.CustomerDefaultStoreID = store;
            return cust;
        }

        private void ViewOrderHistory(Customer cust)
        {
            List<Order> myOrders = _bl.ListOfOrdersByCust(cust);
            List<Product> prodList = _bl.ProductsList();
            // var tempOrdHist = from m1 in prodList 
                // join m2 in myOrders on m1.ProductID equals m2.ProductId
                // select new {m1.ProductID, m2.Name, m1.Quantity, m2.Price,m2.Genre, m2.Description};
        }

       
    
    }
}