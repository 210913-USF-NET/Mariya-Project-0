using System;
using Models;

namespace UI
{
    public class MainMenu :IMenu
    {
        private Customer customer;
         public void Start()
        {
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("Welcome to Books4All");
                Console.WriteLine("[1] Log in to your account");
                Console.WriteLine("[2] Register new user");
                Console.WriteLine("[x] Exit");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ViewAllCustomers();
                        CustomerNavigation();
                        break;
                    case "2":
                        CreateNewCustomer();
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

        private void ViewAllCustomers()
        {
            throw new NotImplementedException();
        }

        private void CreateNewCustomer()
        {
            throw new NotImplementedException();
        }
    }
}