using System;
using System.Collections.Generic;
using System.Linq;
using Models;
using StoreBL;

namespace UI
{
    public class AdminMenu : IMenu
    {
        private IBL _bl;

        public AdminMenu (IBL bl)
        {
            _bl = bl;
        }
        public void Start()
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine($"Welcome Admin menu");
                Console.WriteLine("[1] Order History Menu");
                Console.WriteLine("[2] Update inventory");
                Console.WriteLine("[3] Search existing customers");
                Console.WriteLine("[x] Return to Customer Navigation Menu");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        MenuFactory.GetMenu("hist").Start();
                        break;
                    case "2":
                        UpdateInventoryByStore();
                        break;
                        case "3":
                        SearchCustomers();
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

        private void SearchCustomers()
        {
            List<Customer> loggedIn = new List<Customer>();
            
            Console.WriteLine("Enter Customer Name");
            string useName = Console.ReadLine().ToLower().Trim();
            List<Customer> existingCust = _bl.FindOneCustomer(useName);
            // foreach(Customer user in existingCust){
            //     if (!user.UserName.ToLower().Trim().Contains(useName) || !user.Name.ToLower().Trim().Contains(useName))
            //     {
            //         System.Console.WriteLine("That is not an existing Customer please try another name");
            //         return;
                    
            //     }
            //     break; 
            // }
            //loggedIn.AddRange(existingCust);
            foreach (var item in existingCust)
            {
                Console.WriteLine("==========================================================");
                System.Console.WriteLine(item);
                Console.WriteLine("==========================================================");
            }
            
        }
        private Dictionary<Product, int> AddToInventoryCart(int prodId, int prodQt){
            Product myProd = _bl.GetProduct(prodId);
            ShoppingCart.MyCart.Add(myProd, prodQt);
            foreach (KeyValuePair<Product, int> item in ShoppingCart.MyCart)
                        {
                            Console.WriteLine("**********************************************************");
                            System.Console.WriteLine($"You are adding: {item.Value}\n{item.Key}");
                            Console.WriteLine("**********************************************************");
                        }
            return ShoppingCart.MyCart;
        }
        private void UpdateInventoryByStore()
        {
            bool checkout = false;
            List<StoreFront> allStore = _bl.GetStoreFronts();
            List<Product> prodList = _bl.ProductsList();
            foreach (var item in allStore)
            {
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                System.Console.WriteLine(item);
                Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
            }
            Console.WriteLine("Enter Store Id you want to update:");
            int store = Convert.ToInt32(Console.ReadLine());
            if (!allStore.Any(x => x.StoreID == store))
            {
                Console.WriteLine("That is not a valid entry try again");
                return;
            }
            List<Inventory> invenList = _bl.GetInventoryForAdmin(store);
            // Console.WriteLine("**********************************************************");
            // Console.WriteLine(store);
            // Console.WriteLine("**********************************************************");

            var tempInventory = from m1 in invenList
                join m2 in prodList on m1.ProductID equals m2.ProductId
                select new {m1.ProductID, m2.Name, m1.Quantity, m2.Price,m2.Genre, m2.Description};

                foreach (var item in tempInventory)
            {
                
                System.Console.WriteLine($"Quantity in Stock: {item.Quantity}\nProduct Id: {item.ProductID}\nProduct Name: {item.Name}\nProduct Price: {item.Price:C}\nProduct Genre: {item.Genre}\nProduct Description: {item.Description}\n");
                Console.WriteLine("**********************************************************");
            }
            ShoppingCart: 
            Console.WriteLine("**********************************************************");
            System.Console.WriteLine("Please enter the Product ID :");
            int ItemID = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("Please quantity of Item you want to add");
            int prodQt = Convert.ToInt32(Console.ReadLine());
            AddToInventoryCart(ItemID, prodQt);
            do{
                Console.WriteLine("[1] View List current list of Items");
                Console.WriteLine("[2] Add More items to inventory");
                Console.WriteLine("[3] Confirm Changes");
                Console.WriteLine("[x] Go Back to choose anther Store");
                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        foreach (KeyValuePair<Product, int> item in ShoppingCart.MyCart)
                        {
                            Console.WriteLine("**********************************************************");
                            System.Console.WriteLine($"Your Shopping Cart contains\nQuantity: {item.Value}\n{item.Key}");
                            Console.WriteLine("**********************************************************");
                        }
                        break;
                    case "2":
                        goto ShoppingCart;
                    // case "3":
                    //     System.Console.WriteLine("Please enter the Product ID you want:");
                    //     ItemID = Convert.ToInt32(Console.ReadLine());
                    //     RemoveFromCart(ItemID);
                    //     break;
                    case "3":
                        checkout = true;
                        break;
                    case "x":
                        Console.WriteLine("Have a great day!");
                        
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
                }while (!checkout);
            foreach (KeyValuePair<Product, int> item in ShoppingCart.MyCart)
            {
            
            foreach(Inventory obj in invenList){
                if(obj.ProductID == item.Key.ProductId  && obj.StoreID == store)
                {
                    obj.Quantity = obj.Quantity + item.Value;
                }
            }
        }
        
        _bl.InventorToUpdate(invenList);
        ShoppingCart.MyCart.Clear();
        Console.WriteLine("==========================================================");
        System.Console.WriteLine("Your have updated Inventory");
        Console.WriteLine("==========================================================");
        
        }

    }
}