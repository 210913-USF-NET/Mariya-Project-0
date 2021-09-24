using System;
using Models;
using StoreBL;

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
                Console.WriteLine("[1] To continue login");
                Console.WriteLine("[2] Cancel");
                Console.WriteLine("[x] Go to Main Menu");
                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        ValidateExistingCustomer();
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

        private Customer ValidateExistingCustomer(){
            Start:
            Console.WriteLine("Enter your username");
            string useName = Console.ReadLine();

            Customer user = _bl.FindOneCustomer(useName);
            if(!useName.Equals(user.UserName))
            {
                Console.WriteLine("No such users :/");
                goto Start;
            }
            return user;
        
        }
    }
}