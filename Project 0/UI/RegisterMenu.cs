using System;
using System.Collections.Generic;
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
                Console.WriteLine("[x] Go to Main Menu");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        CreateNewCustomer();
                        // MenuFactory.GetMenuCust("account");
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

        private void CreateNewCustomer(){
            Customer newCustomer = new Customer();
            List <StoreFront> chooseStore = _bl.GetStoreFronts();
            Console.WriteLine("**********************************************************");
            Console.WriteLine("\n           Creating a new user");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("\nEnter your first and last name: ");
            string name = Console.ReadLine();
            newCustomer.Name = name;
            Console.WriteLine("\nEnter a username: ");
            string userName = Console.ReadLine();
            if(userName.Contains("admin")){
                System.Console.WriteLine("That si not a valid name please choose another");
            }
            // else if(userName.Contains)
            newCustomer.UserName = userName;
            Console.WriteLine("\nEnter a password: ");
            newCustomer.Password = Console.ReadLine();
            Console.WriteLine("\nEnter an email address: ");
            newCustomer.Email = Console.ReadLine();
            Console.WriteLine("\nEnter your Address :");
            newCustomer.Address = Console.ReadLine();
            System.Console.WriteLine("List of stores available:");
            foreach (var item in chooseStore)
            {
                System.Console.WriteLine(item);
            }
            Console.WriteLine("Enter your preferred StoreID:");
            int store = Convert.ToInt32(Console.ReadLine());
            newCustomer.CustomerDefaultStoreID = store;
            Customer addedCustomer = _bl.AddCustomer(newCustomer);
            Console.WriteLine("**********************************************************");
            System.Console.WriteLine($"           You created {addedCustomer}");
            Console.WriteLine("**********************************************************");
            // Console.WriteLine($"\nYou created {newCustomer}");
            MenuFactory.GetMenuCust("account").Start(addedCustomer);
            
            
        }
    }
}