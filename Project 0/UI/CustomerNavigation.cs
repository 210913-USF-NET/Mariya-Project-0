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
                // Console.WriteLine("[3] Change your default store");
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
                        // case "3":
                        // shopper = ChangeCustomerStore(shopper);
                        // break;
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

        // private Customer ChangeCustomerStore(Customer cust)
        // {
        //     List <StoreFront> chooseStore = _bl.GetStoreFronts();
        //     System.Console.WriteLine($"Your current StoreID is: {cust.CustomerDefaultStoreID}");
        //     System.Console.WriteLine("List of stores available:");
        //     foreach (var item in chooseStore)
        //     {
        //         System.Console.WriteLine(item);
        //     }
        //     Console.WriteLine("Enter your preferred StoreID:");
        //     int store = Convert.ToInt32(Console.ReadLine());
        //     if (!chooseStore.Any(x => x.StoreID == store))
        //     {
        //         Console.WriteLine("That is not a valid entry try again");
        //         return null;
        //     }
        //     Console.WriteLine($"You entered store ID: {store}\nYour Profile has been updated. ");
        //     cust.CustomerDefaultStoreID = store;
        //     cust = _bl.CustomerStoreUpdate(cust);
        //     return cust;
        // }

        private void ViewOrderHistory(Customer cust)
        {
            List<Order> myOrders = _bl.ListOfOrdersByCust(cust);
            List<LineItem> myLineItems = _bl.LineItemsList();
            List<Product> prodList = _bl.ProductsList();
                var tempOrdHist = from m1 in myLineItems 
                join m2 in prodList on m1.ProductID equals m2.ProductId
                join m3 in myOrders on m1.OrderID equals m3.OrderId
                orderby m3.Date ascending
                select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total};
                foreach(Order ord in myOrders){
                    Console.WriteLine("**********************************************************");
                    Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}");
                    Console.WriteLine("**********************************************************");
                    foreach (var item in tempOrdHist)
                        if(item.OrderId == ord.OrderId)
                        {
                            {
                        System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.Name}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
                        Console.WriteLine("----------------------------------------------------------");;
                        }
                        }
                    Console.WriteLine("**********************************************************");
                    System.Console.WriteLine($" Order Total:{ord.Total:C}");
                    Console.WriteLine("**********************************************************");
                }
        }
    }
}