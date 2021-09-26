using System;
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
                        ViewInventory();
                        break;
                    case "2":
                        StartOrder();
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

        private void StartOrder()
        {
            throw new NotImplementedException();
        }

        private void ViewInventory()
        {
            throw new NotImplementedException();
        }
    }
}