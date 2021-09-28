using System;
using System.Collections.Generic;
using Models;
using StoreBL;

namespace UI
{
    public class MainMenu :IMenu
    {
        // private Customer customer;
         public void Start()
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("**********************************************************");
                Console.WriteLine("           Welcome to Books A Billion");
                Console.WriteLine("**********************************************************");
                
                Console.WriteLine("[1] Log-in an existing user");
                Console.WriteLine("[2] Register new user");
                Console.WriteLine("[x] Exit");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        MenuFactory.GetMenu("log in").Start();
                        // LogInCustomer();
                        // new LoginMenu.Start();
                        break;
                    case "2":
                        MenuFactory.GetMenu("register").Start();
                        // new CustomerNavigation().Start(newCustomer);
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

      
    }
}