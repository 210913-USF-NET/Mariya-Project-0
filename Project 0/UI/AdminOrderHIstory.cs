using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using StoreBL;

namespace UI
{
        public class AdminOrderHIstory : IMenuCust
    {
        private IBL _bl;

        public AdminOrderHIstory (IBL bl)
        {
            _bl = bl;
        }
        
        public void Start(Customer shopper)
        {
            bool exit = false;
            string input = "";
            do
            {
                // Console.WriteLine("[1] Order History By most recent date ");
                // Console.WriteLine("[2] Order History By oldest date");
                // Console.WriteLine("[3] Order History By Total Ascending Date");
                // Console.WriteLine("[4] Order History By Total Ascending");
                // Console.WriteLine("[5] Order History By Customer");
                Console.WriteLine("[6] Order History By Location");
                Console.WriteLine("[x] Exit");
                input = Console.ReadLine();

                switch (input)
                {
                    // case "1":
                    //     OrderHistByAscDate();
                    //     break;
                    // case "2":
                    //     OrderHistByDesDate();
                    //     break;
                    // case "3":
                    //     OrderHistByAscTotal();
                    //     break;
                    // case "4":
                    //     OrderHistByDesTotal();
                    //     break;
                    // case "5":
                    //     OrderHistByCustomer();
                    //     break;
                    case "6":
                        OrderHistByLocation();
                        break;
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

        private void OrderHistByLocation()
        {
            findStore:
            List <StoreFront> chooseStore = _bl.GetStoreFronts();
            foreach (var item in chooseStore)
            {
                Console.WriteLine("**********************************************************");
                System.Console.WriteLine(item);
                Console.WriteLine("**********************************************************");
            }
            System.Console.WriteLine("Please Enter Location ID you want to view orders by:");
            int store = Convert.ToInt32(Console.ReadLine());
            foreach (var item in chooseStore)
            {
                if(!(store == item.StoreID)){
                    System.Console.WriteLine("That is not a valid entry try again");
                    goto findStore;
                }
            }
            List<Order> allOrders = _bl.ListOrder();
            List<LineItem> allLineItems = _bl.LineItemsList();
            List<Product> prodList = _bl.ProductsList();
            List<Customer> customers = _bl.GetAllCustomers();
            //Get Joined list of Order, LineItem and Prod
                var tempOrdHist = from m1 in allLineItems
                where m1.StoreId == store
                join m2 in prodList on m1.ProductID equals m2.ProductId
                join m3 in allOrders on m1.OrderID equals m3.OrderId
                select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, ProdName = m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total };
                
            //Get Joined list of cust and Store
                var tempCustByStore = from m1 in customers
                where m1.CustomerDefaultStoreID == store
                join m2 in chooseStore on m1.CustomerDefaultStoreID equals m2.StoreID
                join m3 in allOrders on m1.CustomerId equals m3.CustomerID
                select new {custName =m1.Name, m1.UserName, storeName= m2.Name, m2.Address};
                foreach(Order ord in tempCustByStore){
                   
                    Console.WriteLine("**********************************************************");
                    Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}\n Customer Name: {}");
                    Console.WriteLine("**********************************************************");
                    foreach (var item in tempOrdHist)
                    {
                        if(item.OrderId == ord.OrderId)
                        {
                            
                        System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.ProdName}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
                        Console.WriteLine("----------------------------------------------------------");;
                        
                        }
                    Console.WriteLine("**********************************************************");
                    System.Console.WriteLine($" Order Total:{ord.Total:C}");
                    Console.WriteLine("**********************************************************");
                    }
                    
                
                }
        }
    

//         private void OrderHistByCustomer()
//         {
//             List<Order> myOrders = _bl.ListOfOrdersByCust(cust);
//             List<LineItem> myLineItems = _bl.LineItemsList();
//             List<Product> prodList = _bl.ProductsList();
//                 var tempOrdHist = from m1 in myLineItems 
//                 join m2 in prodList on m1.ProductID equals m2.ProductId
//                 join m3 in myOrders on m1.OrderID equals m3.OrderId
//                 orderby m3.Date ascending
//                 select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total};
//                 foreach(Order ord in myOrders){
//                     Console.WriteLine("**********************************************************");
//                     Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}");
//                     Console.WriteLine("**********************************************************");
//                     foreach (var item in tempOrdHist)
//                         if(item.OrderId == ord.OrderId)
//                         {
//                             {
//                         System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.Name}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
//                         Console.WriteLine("----------------------------------------------------------");;
//                         }
//                         }
//                     Console.WriteLine("**********************************************************");
//                     System.Console.WriteLine($" Order Total:{ord.Total:C}");
//                     Console.WriteLine("**********************************************************");
//                 }
//         }

//         private void OrderHistByDesTotal()
//         {
//             List<Order> myOrders = _bl.ListOfOrdersByCust(cust);
//             List<LineItem> myLineItems = _bl.LineItemsList();
//             List<Product> prodList = _bl.ProductsList();
//                 var tempOrdHist = from m1 in myLineItems 
//                 join m2 in prodList on m1.ProductID equals m2.ProductId
//                 join m3 in myOrders on m1.OrderID equals m3.OrderId
//                 orderby m3.Date ascending
//                 select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total};
//                 foreach(Order ord in myOrders){
//                     Console.WriteLine("**********************************************************");
//                     Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}");
//                     Console.WriteLine("**********************************************************");
//                     foreach (var item in tempOrdHist)
//                         if(item.OrderId == ord.OrderId)
//                         {
//                             {
//                         System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.Name}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
//                         Console.WriteLine("----------------------------------------------------------");;
//                         }
//                         }
//                     Console.WriteLine("**********************************************************");
//                     System.Console.WriteLine($" Order Total:{ord.Total:C}");
//                     Console.WriteLine("**********************************************************");
//                 }
//         }

//         private void OrderHistByAscTotal()
//         {
//             List<Order> myOrders = _bl.ListOfOrdersByCust(cust);
//             List<LineItem> myLineItems = _bl.LineItemsList();
//             List<Product> prodList = _bl.ProductsList();
//                 var tempOrdHist = from m1 in myLineItems 
//                 join m2 in prodList on m1.ProductID equals m2.ProductId
//                 join m3 in myOrders on m1.OrderID equals m3.OrderId
//                 orderby m3.Date ascending
//                 select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total};
//                 foreach(Order ord in myOrders){
//                     Console.WriteLine("**********************************************************");
//                     Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}");
//                     Console.WriteLine("**********************************************************");
//                     foreach (var item in tempOrdHist)
//                         if(item.OrderId == ord.OrderId)
//                         {
//                             {
//                         System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.Name}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
//                         Console.WriteLine("----------------------------------------------------------");;
//                         }
//                         }
//                     Console.WriteLine("**********************************************************");
//                     System.Console.WriteLine($" Order Total:{ord.Total:C}");
//                     Console.WriteLine("**********************************************************");
//                 }
//         }

//         private void OrderHistByDesDate()
//         {
//             List<Order> myOrders = _bl.ListOfOrdersByCust(cust);
//             List<LineItem> myLineItems = _bl.LineItemsList();
//             List<Product> prodList = _bl.ProductsList();
//                 var tempOrdHist = from m1 in myLineItems 
//                 join m2 in prodList on m1.ProductID equals m2.ProductId
//                 join m3 in myOrders on m1.OrderID equals m3.OrderId
//                 orderby m3.Date ascending
//                 select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total};
//                 foreach(Order ord in myOrders){
//                     Console.WriteLine("**********************************************************");
//                     Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}");
//                     Console.WriteLine("**********************************************************");
//                     foreach (var item in tempOrdHist)
//                         if(item.OrderId == ord.OrderId)
//                         {
//                             {
//                         System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.Name}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
//                         Console.WriteLine("----------------------------------------------------------");;
//                         }
//                         }
//                     Console.WriteLine("**********************************************************");
//                     System.Console.WriteLine($" Order Total:{ord.Total:C}");
//                     Console.WriteLine("**********************************************************");
//                 }
//         }

//         private void OrderHistByAscDate()
//         {
//             List<Order> myOrders = _bl.ListOfOrdersByCust(cust);
//             List<LineItem> myLineItems = _bl.LineItemsList();
//             List<Product> prodList = _bl.ProductsList();
//                 var tempOrdHist = from m1 in myLineItems 
//                 join m2 in prodList on m1.ProductID equals m2.ProductId
//                 join m3 in myOrders on m1.OrderID equals m3.OrderId
//                 orderby m3.Date ascending
//                 select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total};
//                 foreach(Order ord in myOrders){
//                     Console.WriteLine("**********************************************************");
//                     Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}");
//                     Console.WriteLine("**********************************************************");
//                     foreach (var item in tempOrdHist)
//                         if(item.OrderId == ord.OrderId)
//                         {
//                             {
//                         System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.Name}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
//                         Console.WriteLine("----------------------------------------------------------");;
//                         }
//                         }
//                     Console.WriteLine("**********************************************************");
//                     System.Console.WriteLine($" Order Total:{ord.Total:C}");
//                     Console.WriteLine("**********************************************************");
//                 }
//         }
    }
}