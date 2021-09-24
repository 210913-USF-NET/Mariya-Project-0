using System;
namespace UI
{
    public class MainMenu : IMenu
    {
        public void Start() {
            Console.WriteLine("This is the Main Menu");
            bool exit = false;
            string userInput = "";
            do
            {
                Console.WriteLine("[1] Log-in an existing user");
                Console.WriteLine("[2] Register new user");
                Console.WriteLine("[3] Admin Log-in");
                Console.WriteLine("[x] Exit");
                userInput = Console.ReadLine();
                switch (userInput)
                {
                    case "1":
                    System.Console.WriteLine("Log in");
                        // CheckExistingUser();
                        break;
                    case "2":
                    System.Console.WriteLine("Register");
                        // RegisterNewUser();
                        break;
                    case "3":
                    System.Console.WriteLine("Admin login");
                        // AdminLogInMenu();
                        break;
                    case "x":
                        Console.WriteLine("Exit program.");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Ivalid input.");
                        break;
                }

            }while (!exit);
        }
    }
}