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
                Main:
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
            Start:
            Console.WriteLine("\nEnter your username");
            string useName = Console.ReadLine();

            var users = _bl.FindOneCustomer(useName);
            if(users == null || users.Count == 0)
            {
                Console.WriteLine("No such users :/");
            }
           
            Customer loggedIn = new Customer();
            loggedIn = users.Where(c => c.UserName.Equals(useName)).FirstOrDefault();
            Console.WriteLine("**********************************************************");
            Console.WriteLine($"           Welcome {loggedIn.Name}!");
            Console.WriteLine("**********************************************************");
            // foreach(Customer u in users){
            //     System.Console.WriteLine($"Hello {u.Name}");
            // }
        

            MenuFactory.GetMenuCust("account").Start(loggedIn);
            
        }

         private void ValidateAdmin(){
          
            Console.WriteLine("Enter your username");
            string useName = Console.ReadLine();
            if( !useName.Equals("Admin")){
                System.Console.WriteLine("That is not a valid input");
                return;
            }
            var users = _bl.FindOneCustomer(useName);
            Customer loggedIn = new Customer();
            loggedIn = users.Where(c => c.UserName.Equals(useName)).FirstOrDefault();
            Console.WriteLine("**********************************************************");
            Console.WriteLine($"Welcome {loggedIn.Name}!");
            Console.WriteLine("**********************************************************");
            MenuFactory.GetMenuCust("admin").Start(loggedIn);
        
        
        }
    }
}