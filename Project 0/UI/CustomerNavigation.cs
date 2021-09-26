using System;
using Models;
using StoreBL;

namespace UI
{
    public class CustomerNavigation  : IMenuCust
    {
        private IBL _bl;
        private Customer _shopper;
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
                        ViewOrderHistory();
                        break;
                        case "3":
                        ChangeCustomerStore();
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

        private void ChangeCustomerStore()
        {
            throw new NotImplementedException();
        }

        private void ViewOrderHistory()
        {
            throw new NotImplementedException();
        }

       
    
    }
}