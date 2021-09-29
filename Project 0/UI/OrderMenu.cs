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
                        Console.WriteLine("Thanks for shopping with us!");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Invalid input");
                        break;
                }
            } while (!exit);

        }

        private Dictionary<Product, int> AddToCart(int prodId, int prodQt){
            Product myProd = _bl.GetProduct(prodId);
            ShoppingCart.MyCart.Add(myProd, prodQt);
            foreach (KeyValuePair<Product, int> item in ShoppingCart.MyCart)
                        {
                            Console.WriteLine("**********************************************************");
                            System.Console.WriteLine($"You selected Quantity: {item.Value}\n{item.Key}");
                            Console.WriteLine("**********************************************************");
                        }
            return ShoppingCart.MyCart;
        }
        private void StartOrder(Customer cust)
        {
            int prodQt;
            int ItemID; 
            bool checkout = false;
            string input = "";
            //a way to temp store prod and qt while shopping
            List <Inventory> inventorToUpdate = ViewInventory(cust);
            do
            {
            ShoppingCart:    
            Console.WriteLine("**********************************************************");
            System.Console.WriteLine("Please enter the Product ID you want:");
            ItemID = Convert.ToInt32(Console.ReadLine());
            System.Console.WriteLine("Please enter the Product Quantity you want:");
            prodQt = Convert.ToInt32(Console.ReadLine());
            AddToCart(ItemID, prodQt);
                do{
                Console.WriteLine("[1] View Oder");
                Console.WriteLine("[2] Add More items to your cart");
                // Console.WriteLine("[3] Remove items from your cart");
                Console.WriteLine("[4] Checkout");
                Console.WriteLine("[x] Go to Main Menu");
                input = Console.ReadLine();

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
                    case "4":
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
            } while (!checkout);
            Order newOrder = createOrdder(cust);
            Order addedOrder = _bl.AddNewOrder(newOrder);
            //ShoppingCart.MyCart  Dictionary<int, Product> cart
            foreach (KeyValuePair<Product, int> item in ShoppingCart.MyCart)
                        {
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                            System.Console.WriteLine($"You selected Quantity: {item.Value}\n{item.Key}");
                            Console.WriteLine("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
                        }
        Console.WriteLine($"Your OrderID is: {addedOrder.OrderId}\nYour order total is {addedOrder.Total:C}!");
        Console.WriteLine("**********************************************************");
        List<LineItem> lineitems = new List<LineItem>();
        
        foreach (KeyValuePair<Product, int> item in ShoppingCart.MyCart)
        {
            lineitems.Add(new LineItem(){
                OrderID = addedOrder.OrderId,
                StoreId = cust.CustomerDefaultStoreID,
                ProductID = item.Key.ProductId,
                Quantity = item.Value
            });
            foreach(Inventory obj in inventorToUpdate){
                if(obj.ProductID == item.Key.ProductId  && obj.StoreID == cust.CustomerDefaultStoreID)
                {
                    obj.Quantity = obj.Quantity -item.Value;
                }
            }
        }
        _bl.AddLineItems(lineitems);
        _bl.InventorToUpdate(inventorToUpdate);
        ShoppingCart.MyCart.Clear();
        }
    private Order createOrdder(Customer cust){
        decimal total = ShoppingCart.MyCart.Sum(x => x.Key.Price);
        Order newOrd = new Order();
        newOrd.CustomerID = cust.CustomerId;
        newOrd.Total = total;
        newOrd.StoreID = cust.CustomerDefaultStoreID;
        //call BL/DL method to create order in DB -> and make sure you are getting that id of the new order
        //once you have the id
        List<LineItem> lineitems = new List<LineItem>();
        Console.WriteLine("**********************************************************");
        System.Console.WriteLine($"Thank you for your purchase {cust.Name}!");
        System.Console.WriteLine("Your Order contains:");
        return newOrd;
    }
        private List<Inventory> ViewInventory(Customer cust)
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

            return storeInven;
        }
    }
}