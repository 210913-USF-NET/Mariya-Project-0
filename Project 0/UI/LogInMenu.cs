using System;
using System.Collections.Generic;
using Models;
using StoreBL;
using System.Linq;

namespace UI
{
    public class LogInMenu : IMenu
    {
       
        private IBL _bl;
        public LogInMenu(IBL bl)
        {
            _bl = bl;
        }
        
        public void Start()
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("[1] Log as Customer");
                Console.WriteLine("[2] log in as Admin");
                Console.WriteLine("[x] Go to Main Menu");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ValidateExistingCustomer();
                        MenuFactory.GetMenuCust("account");
                        break;
                    case "2":
                        ValidateAdmin();
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

        private void ValidateExistingCustomer(){
            Customer loggedIn = new Customer();
            Console.WriteLine("\nEnter your username");
            string useName = Console.ReadLine().ToLower().Trim();
            List<Customer> existingCust = _bl.FindOneCustomer(useName);
            foreach(Customer user in existingCust){
                if (!user.UserName.ToLower().Trim().Contains(useName))
                {
                    System.Console.WriteLine("That is not a valid username please input a correct username or Register as a new user");
                    return;
                }
                loggedIn = existingCust.Where(c => c.UserName.Trim().ToLower().Equals(useName)).FirstOrDefault();
            }
            if(existingCust == null || existingCust.Count == 0)
            {
                Console.WriteLine("No such users :/");
            }
            else if (useName.ToLower().Trim().Equals("admin")){
                ValidateAdmin();
            }

            
            
            Console.WriteLine("**********************************************************");
            Console.WriteLine($"           Welcome {loggedIn.Name}!");
            Console.WriteLine("**********************************************************");
            // foreach(Customer u in users){
            //     System.Console.WriteLine($"Hello {u.Name}");
            // }
        

            MenuFactory.GetMenuCust("account").Start(loggedIn);
            
        }

        private void ValidateAdmin(){
            Customer loggedIn = new Customer();
            Console.WriteLine("Enter Administrator Log In");
            string useName = Console.ReadLine().ToLower().Trim();
            List<Customer> existingCust = _bl.FindOneCustomer(useName);
            foreach(Customer user in existingCust){
                if (!user.UserName.ToLower().Trim().Equals("admin"))
                {
                    System.Console.WriteLine("Please enter Admin log in credentials");
                    return;
                }
                loggedIn = existingCust.Where(c => c.UserName.Equals(useName)).FirstOrDefault();
            }
            if(existingCust == null || existingCust.Count == 0)
            {
                Console.WriteLine("No such users :/");
                return;
            }
            
            MenuFactory.GetMenu("Admin").Start();
        }
    }
}