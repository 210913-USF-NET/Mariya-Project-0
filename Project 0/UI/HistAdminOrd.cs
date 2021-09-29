using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using StoreBL;

namespace UI
{
        public class HistAdminOrd : IMenu
    {
        private IBL _bl;

        public HistAdminOrd (IBL bl)
        {
            _bl = bl;
        }
        
        public void Start()
        {
            bool exit = false;
            string input = "";
            do
            {
                System.Console.WriteLine("History Admin Menu");
                Console.WriteLine("[1] Order History By most recent date ");
                Console.WriteLine("[2] Order History By oldest date");
                Console.WriteLine("[3] Order History By Total Ascending Date");
                Console.WriteLine("[4] Order History By Total Descending");
                Console.WriteLine("[5] Order History By Customer");
                Console.WriteLine("[6] Order History By Location");
                Console.WriteLine("[x] Return to Admin Menu");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        OrderHistByAscDate();
                        break;
                    case "2":
                        OrderHistDateDesc();
                        break;
                    case "3":
                        OrderHistByAscTotal();
                        break;
                    case "4":
                        OrderHistByDesTotal();
                        break;
                    case "5":
                        OrderHistByCustomer();
                        break;
                    case "6":
                        OrderHistByLocation();
                        break;
                    case "x":
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
            
            List <StoreFront> chooseStore = _bl.GetStoreFronts();
            foreach (var item in chooseStore)
            {
                Console.WriteLine("**********************************************************");
                System.Console.WriteLine(item);
                Console.WriteLine("**********************************************************");
            }
            System.Console.WriteLine("Please Enter Location ID you want to view orders by:");
            int store = Convert.ToInt32(Console.ReadLine());
           if (!chooseStore.Any(x => x.StoreID == store))
            {
                Console.WriteLine("That is not a valid entry try again");
                return;
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
                select new {m2.Address, custName =m1.Name, m1.UserName, storeName= m2.Name, storeAd= m2.Address, m3.OrderId, m3.Date, m3.Total,m1.CustomerDefaultStoreID};
                foreach(var ord in tempCustByStore){
                    Console.WriteLine("**********************************************************");
                    Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}\nCustomer Name: {ord.custName}\nStoreID:{ord.CustomerDefaultStoreID}\nStoreName:{ord.storeName}\nStore Address: {ord.storeAd}");
                    Console.WriteLine("**********************************************************");
                    foreach (var item in tempOrdHist)
                    {
                        if(item.OrderId == ord.OrderId)
                        {
                            
                        System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.ProdName}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
                        Console.WriteLine("----------------------------------------------------------");;
                        
                        }
                    
                    }
                    Console.WriteLine("**********************************************************");
                    System.Console.WriteLine($"Order Total:{ord.Total:C}");
                    Console.WriteLine("**********************************************************");
                
                }
        }
    
/// <summary>
/// This takes Pulls list of Order, Customer, Line item and product
/// joins them and then uses 2 foreach loops to iterate through print each customer by 
/// </summary>
        private void OrderHistByCustomer()
        {
            Customer loggedIn = new Customer();
            Console.WriteLine("Enter Customer Name");
            string useName = Console.ReadLine().ToLower().Trim();
            List<Customer> existingCust = _bl.FindOneCustomer(useName);
            if (existingCust.Any(x => x.UserName.ToLower().Trim().Contains(useName) || x.Name.ToLower().Trim().Contains(useName)))
            {
                System.Console.WriteLine("That is not an existing Customer please try another name");
                    return;
            }
           
                loggedIn = existingCust.Where(c => c.UserName.Trim().ToLower().Equals(useName) || c.Name.Trim().ToLower().Contains(useName)).FirstOrDefault();
           
            // foreach(Customer user in existingCust){
            //     if (user.UserName.ToLower().Trim().Contains(useName) || user.Name.ToLower().Trim().Contains(useName))
            //     {
            //         loggedIn = existingCust.Where(c => c.UserName.Trim().ToLower().Equals(useName)|| c.Name.Trim().ToLower().Contains(useName)).FirstOrDefault();
            //         break;
            //     }
                   
                
            // }
            
            List <StoreFront> chooseStore = _bl.GetStoreFronts();
            List<Order> allOrders = _bl.ListOrder();
            List<LineItem> allLineItems = _bl.LineItemsList();
            List<Product> prodList = _bl.ProductsList();
            List<Customer> customers = _bl.GetAllCustomers();
            //Get Joined list of Order, LineItem and Prod
                var tempOrdHist = from m1 in allLineItems
                join m2 in prodList on m1.ProductID equals m2.ProductId
                join m3 in allOrders on m1.OrderID equals m3.OrderId
                select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, ProdName = m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total };
                
            //Get Joined list of cust and Store
                var tempCustByStore = from m1 in customers
                where m1.UserName.ToLower().Trim().Contains(loggedIn.UserName.ToLower().Trim())
                join m2 in chooseStore on m1.CustomerDefaultStoreID equals m2.StoreID
                join m3 in allOrders on m1.CustomerId equals m3.CustomerID
                select new {m2.Address, custName =m1.Name, m1.UserName, storeName= m2.Name, storeAd= m2.Address, m3.OrderId, m3.Date, m3.Total,m1.CustomerDefaultStoreID};
                foreach(var ord in tempCustByStore){
                    if(ord.UserName.ToLower().Trim().Contains(loggedIn.UserName.Trim().ToLower()))
                        {
                        Console.WriteLine("**********************************************************");
                        Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}\nCustomer Name: {ord.custName}\nStoreID:{ord.CustomerDefaultStoreID}\nStoreName:{ord.storeName}\nStore Address: {ord.storeAd}");
                        Console.WriteLine("**********************************************************");
                            foreach (var item in tempOrdHist)
                            {
                                if(item.OrderId == ord.OrderId)
                            {
                            
                        System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.ProdName}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
                        Console.WriteLine("----------------------------------------------------------");;
                        
                        }
                    
                    }
                    Console.WriteLine("**********************************************************");
                    System.Console.WriteLine($"Order Total:{ord.Total:C}");
                    Console.WriteLine("**********************************************************");
                
                }
                }

        }
        private void OrderHistByAscTotal()
        {
            List<StoreFront> allStore= _bl.GetStoreFronts();
            List<Customer> customers = _bl.GetAllCustomers();
            List<Order> myOrders = _bl.ListOrder();
            List<LineItem> myLineItems = _bl.LineItemsList();
            List<Product> prodList = _bl.ProductsList();
            var tempOrdHist = from m1 in myLineItems
                join m2 in prodList on m1.ProductID equals m2.ProductId
                join m3 in myOrders on m1.OrderID equals m3.OrderId
                orderby m3.Total ascending
                select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, ProdName = m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total };
                
            //Get Joined list of cust and Store
                var tempCustByStore = from m1 in customers
                join m2 in allStore on m1.CustomerDefaultStoreID equals m2.StoreID
                join m3 in myOrders on m1.CustomerId equals m3.CustomerID
                orderby m3.Total ascending
                select new {m2.Address, custName =m1.Name, m1.UserName, storeName= m2.Name, storeAd= m2.Address, m3.OrderId, m3.Date, m3.Total,m1.CustomerDefaultStoreID};
                foreach(var ord in tempCustByStore){
                    
                        Console.WriteLine("**********************************************************");
                        Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}\nCustomer Name: {ord.custName}\nStoreID:{ord.CustomerDefaultStoreID}\nStoreName:{ord.storeName}\nStore Address: {ord.storeAd}");
                        Console.WriteLine("**********************************************************");
                            foreach (var item in tempOrdHist)
                            {
                                if(item.OrderId == ord.OrderId)
                            {
                            
                        System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.ProdName}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
                        Console.WriteLine("----------------------------------------------------------");;
                        
                        }
                    
                    }
                    Console.WriteLine("**********************************************************");
                    System.Console.WriteLine($"Order Total:{ord.Total:C}");
                    Console.WriteLine("**********************************************************");
                
                
                
                }
        }
        private void OrderHistByDesTotal()
        {
            List<StoreFront> allStore= _bl.GetStoreFronts();
            List<Customer> customers = _bl.GetAllCustomers();
            List<Order> myOrders = _bl.ListOrder();
            List<LineItem> myLineItems = _bl.LineItemsList();
            List<Product> prodList = _bl.ProductsList();
            var tempOrdHist = from m1 in myLineItems
                join m2 in prodList on m1.ProductID equals m2.ProductId
                join m3 in myOrders on m1.OrderID equals m3.OrderId
                orderby m3.Total descending
                select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, ProdName = m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total };
                
            //Get Joined list of cust and Store
                var tempCustByStore = from m1 in customers
                join m2 in allStore on m1.CustomerDefaultStoreID equals m2.StoreID
                join m3 in myOrders on m1.CustomerId equals m3.CustomerID
                orderby m3.Total descending
                select new {m2.Address, custName =m1.Name, m1.UserName, storeName= m2.Name, storeAd= m2.Address, m3.OrderId, m3.Date, m3.Total,m1.CustomerDefaultStoreID};
                foreach(var ord in tempCustByStore){
                    
                        Console.WriteLine("**********************************************************");
                        Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}\nCustomer Name: {ord.custName}\nStoreID:{ord.CustomerDefaultStoreID}\nStoreName:{ord.storeName}\nStore Address: {ord.storeAd}");
                        Console.WriteLine("**********************************************************");
                            foreach (var item in tempOrdHist)
                            {
                                if(item.OrderId == ord.OrderId)
                            {
                            
                        System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.ProdName}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
                        Console.WriteLine("----------------------------------------------------------");;
                        
                        }
                    
                    }
                    Console.WriteLine("**********************************************************");
                    System.Console.WriteLine($"Order Total:{ord.Total:C}");
                    Console.WriteLine("**********************************************************");
                
                
                
                }
        }
        private void OrderHistDateDesc()
        {
            List<StoreFront> allStore= _bl.GetStoreFronts();
            List<Customer> customers = _bl.GetAllCustomers();
            List<Order> myOrders = _bl.ListOrder();
            List<LineItem> myLineItems = _bl.LineItemsList();
            List<Product> prodList = _bl.ProductsList();
            var tempOrdHist = from m1 in myLineItems
                join m2 in prodList on m1.ProductID equals m2.ProductId
                join m3 in myOrders on m1.OrderID equals m3.OrderId
                orderby m3.Date descending
                select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, ProdName = m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total };
                
            //Get Joined list of cust and Store
                var tempCustByStore = from m1 in customers
                join m2 in allStore on m1.CustomerDefaultStoreID equals m2.StoreID
                join m3 in myOrders on m1.CustomerId equals m3.CustomerID
                orderby m3.Date descending
                select new {m2.Address, custName =m1.Name, m1.UserName, storeName= m2.Name, storeAd= m2.Address, m3.OrderId, m3.Date, m3.Total,m1.CustomerDefaultStoreID};
                foreach(var ord in tempCustByStore){
                    
                        Console.WriteLine("**********************************************************");
                        Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}\nCustomer Name: {ord.custName}\nStoreID:{ord.CustomerDefaultStoreID}\nStoreName:{ord.storeName}\nStore Address: {ord.storeAd}");
                        Console.WriteLine("**********************************************************");
                            foreach (var item in tempOrdHist)
                            {
                                if(item.OrderId == ord.OrderId)
                            {
                            
                        System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.ProdName}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
                        Console.WriteLine("----------------------------------------------------------");;
                        
                        }
                    
                    }
                    Console.WriteLine("**********************************************************");
                    System.Console.WriteLine($"Order Total:{ord.Total:C}");
                    Console.WriteLine("**********************************************************");
                
                
                
                }
        }
        private void OrderHistByAscDate()
        {
            List<StoreFront> allStore= _bl.GetStoreFronts();
            List<Customer> customers = _bl.GetAllCustomers();
            List<Order> myOrders = _bl.ListOrder();
            List<LineItem> myLineItems = _bl.LineItemsList();
            List<Product> prodList = _bl.ProductsList();
            var tempOrdHist = from m1 in myLineItems
                join m2 in prodList on m1.ProductID equals m2.ProductId
                join m3 in myOrders on m1.OrderID equals m3.OrderId
                orderby m3.Date ascending
                select new {m3.OrderId, m3.Date, m3.StoreID, m1.Quantity, m1.ProductID, ProdName = m2.Name,  m2.Price, m2.Genre, m2.Description, m3.Total };
                
            //Get Joined list of cust and Store
                var tempCustByStore = from m1 in customers
                join m2 in allStore on m1.CustomerDefaultStoreID equals m2.StoreID
                join m3 in myOrders on m1.CustomerId equals m3.CustomerID
                orderby m3.Date ascending
                select new {m2.Address, custName =m1.Name, m1.UserName, storeName= m2.Name, storeAd= m2.Address, m3.OrderId, m3.Date, m3.Total,m1.CustomerDefaultStoreID};
                foreach(var ord in tempCustByStore){
                    
                        Console.WriteLine("**********************************************************");
                        Console.WriteLine($"Order ID: {ord.OrderId} Order Date: {ord.Date}\nCustomer Name: {ord.custName}\nStoreID:{ord.CustomerDefaultStoreID}\nStoreName:{ord.storeName}\nStore Address: {ord.storeAd}");
                        Console.WriteLine("**********************************************************");
                            foreach (var item in tempOrdHist)
                            {
                                if(item.OrderId == ord.OrderId)
                            {
                            
                        System.Console.WriteLine($"Product Quantity Purchased: {item.Quantity}\nProduct Name: {item.ProdName}\nProduct Id: {item.ProductID}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
                        Console.WriteLine("----------------------------------------------------------");;
                        
                        }
                    
                    }
                    Console.WriteLine("**********************************************************");
                    System.Console.WriteLine($"Order Total:{ord.Total:C}");
                    Console.WriteLine("**********************************************************");
                
                
                
                }
        }
    }
}