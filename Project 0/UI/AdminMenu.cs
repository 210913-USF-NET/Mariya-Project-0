using System;
using Models;

namespace UI
{
    public class AdminMenu : IMenuCust
    {
        public void Start(Customer shopper)
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine($"Welcome Admin");
                Console.WriteLine("[1] Add new Location");
                Console.WriteLine("[2] View location order history");
                Console.WriteLine("[3] update inventory");
                Console.WriteLine("[4] Search existing customers");
                Console.WriteLine("[x] Exit");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        AddNewLocation();
                        break;
                    case "2":
                        OrderHistByLocation();
                        break;
                        case "3":
                        UpdateInventoryByStore();
                        break;
                        case "4":
                        SearchCustomers();
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

        private void SearchCustomers()
        {
            throw new NotImplementedException();
        }

        private void UpdateInventoryByStore()
        {
            throw new NotImplementedException();
        }

        private void OrderHistByLocation()
        {
            throw new NotImplementedException();
        }

        private void AddNewLocation()
        {
            throw new NotImplementedException();
        }
    }
}