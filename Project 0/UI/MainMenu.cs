using System;

namespace UI
{
    public class MainMenu : IMenu
    {
        void Start(){
            bool exit = false;
            string input = "";
            do
            {
                Console.WriteLine("Welcome to Books4All");
                Console.WriteLine("[1] Log in to your account");
                Console.WriteLine("[2] Register new user");
                Console.WriteLine("[x] Exit");
        }

                input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        MenuFactory.GetMenu("restaurant").Start();
                        break;

                    case "2":
                        MenuFactory.GetMenu("review").Start();
                        break;

                    case "x":
                        Console.WriteLine("Goodbye!");
                        exit = true;
                        break;

                    default:
                        Console.WriteLine("I don't know what you're talking about");
                        break;
                }
            } while (!exit);
         
    }
}