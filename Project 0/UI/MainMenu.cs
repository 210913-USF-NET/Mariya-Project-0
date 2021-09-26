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
                Console.WriteLine("Welcome to Books A Billion");
                Main:
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

/// <summary>
/// Prompts log in and checks input against what is in the DB
/// </summary>
/// <returns>Cutomer model from DB</returns>
   
        // private void CreateNewCustomer(){
        //     Customer newCustomer = new Customer();

        //     Console.WriteLine("Creating a new user");

        //     Console.WriteLine("\nEnter your first and last name: ");
        //     string name = Console.ReadLine();
        //     newCustomer.Name = name;

        //     Console.WriteLine("\nEnter a username: ");
        //     string username = Console.ReadLine();
        //     newCustomer.UserName = username;

        //     Console.WriteLine("\nEnter a password: ");
        //     string password = Console.ReadLine();
        //     newCustomer.Password = password;

        //     Console.WriteLine("\nEnter an email address: ");
        //     string email = Console.ReadLine();
        //     newCustomer.Email = email;

        //     Console.WriteLine("\nEnter your Address :");
        //     string address = Console.ReadLine();
        //     newCustomer.Address = address;

        //     Console.WriteLine("\nEnter your StoreID:");
        //     int store = Convert.ToInt32(Console.ReadLine());
        //     newCustomer.CustomerDefaultStoreID = store;
            
        //     Customer addedCustomer = _bl.AddCustomer(newCustomer);
        //     System.Console.WriteLine($"You created {addedCustomer}");
    
        //     // Console.WriteLine($"\nYou created {newCustomer}");
        //     // return addedCustomer;

        // }
      
    }
}