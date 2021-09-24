using System;
using Models;
using StoreBL;

namespace UI
{
    public class RegisterMenu : IMenu

    {
        private IBL _bl;
        public RegisterMenu(IBL bl)
        {
            _bl = bl;
        }
        
        public void Start()
        {
            bool exit = false;
            string input = "";
            do
            {
                Main:
                Console.WriteLine("[1] Start registration");
                Console.WriteLine("[2] Cancel");
                Console.WriteLine("[x] Go to Main Menu");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateNewCustomer();
                        break;
                    case "2":
                        exit = true;
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

        private Customer CreateNewCustomer(){
            Customer newCustomer = new Customer();

            Console.WriteLine("Creating a new user");

            Console.WriteLine("\nEnter your first and last name: ");
            string name = Console.ReadLine();
            newCustomer.Name = name;

            Console.WriteLine("\nEnter a username: ");
            string username = Console.ReadLine();
            newCustomer.UserName = username;

            Console.WriteLine("\nEnter a password: ");
            string password = Console.ReadLine();
            newCustomer.Password = password;

            Console.WriteLine("\nEnter an email address: ");
            string email = Console.ReadLine();
            newCustomer.Email = email;

            Console.WriteLine("\nEnter your Address :");
            string address = Console.ReadLine();
            newCustomer.Address = address;

            Console.WriteLine("\nEnter your StoreID:");
            int store = Convert.ToInt32(Console.ReadLine());
            newCustomer.CustomerDefaultStoreID = store;
            
            Customer addedCustomer = _bl.AddCustomer(newCustomer);
            System.Console.WriteLine($"You created {addedCustomer}");
    
            // Console.WriteLine($"\nYou created {newCustomer}");
            return addedCustomer;

        }
    }
}