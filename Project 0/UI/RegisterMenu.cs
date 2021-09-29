using System;
using System.Collections.Generic;
using System.Linq;
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
            List<Customer> existingCust = _bl.GetAllCustomers();
            Customer newCustomer = new Customer();
            List <StoreFront> chooseStore = _bl.GetStoreFronts();
            Console.WriteLine("**********************************************************");
            Console.WriteLine("           Creating a new user");
            Console.WriteLine("**********************************************************");
            Console.WriteLine("\nEnter your first and last name: ");
            string name = Console.ReadLine();
            foreach(Customer user in existingCust)
            {
                if (user.Name.ToLower().Trim().Equals(name.ToLower().Trim()))
                {
                    System.Console.WriteLine("That Name is already registered please log in or enter a different name");
                    return;
                }
                else newCustomer.Name = name;
            }
            
            if(existingCust == null || existingCust.Count == 0)
            {
                Console.WriteLine("No such users :/");
                return;
            }
            //need to go through and search for existing customers by name and username
            Console.WriteLine("\nEnter a username: ");
            string userName = Console.ReadLine();
            foreach(Customer user in existingCust){
                if (user.UserName.ToLower().Trim().Equals(userName.ToLower().Trim()))
                {
                    System.Console.WriteLine("That User Name is already registered please log in or enter a different name");
                    return;
                }
                else
                {
                    newCustomer.UserName = userName;
                }
                }
            
            
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
            if (!chooseStore.Any(x => x.StoreID == store))
            {
                Console.WriteLine("That is not a valid entry try again");
                return;
            }
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