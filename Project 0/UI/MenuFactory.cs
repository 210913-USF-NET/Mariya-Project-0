using DL.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using DL;
using StoreBL;
namespace UI
{
    public class MenuFactory
    {
        public static IMenu GetMenu(string menuString)
        {
        
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<Project00Context> options = new DbContextOptionsBuilder<Project00Context>()
            .UseSqlServer(connectionString).Options;
            Project00Context context = new Project00Context(options);


    //         //this is an example of dependency injection
    //         //I'm "injecting" an instance of business logic layer to restaurant menu, and an implementation of 
    //         // IRepo to business logic
    //         // IRepo dataLayer = new FileRepo();
    //         // IBL businessLogic = new BL(dataLayer);
    //         // IMenu restaurantMenu = new RestaurantMenu(businessLogic);

            switch (menuString.ToLower()) 
            {
                case "main":
                   // return new MainMenu();
                case "register":
                    return new RegisterMenu(new BL(new Repo(context)));
                case "log in":
                    return new LogInMenu(new BL(new Repo(context)));
                case "hist":
                    return new HistAdminOrd(new BL(new Repo(context)));
                case "admin":
                    return new AdminMenu(new BL(new Repo(context)));
                default:
                    return null;
            }
        }
         public static IMenuCust GetMenuCust(string menuString)
        {
            Customer loggedIn = new Customer();
            string connectionString = File.ReadAllText(@"../connectionString.txt");
            DbContextOptions<Project00Context> options = new DbContextOptionsBuilder<Project00Context>()
            .UseSqlServer(connectionString).Options;
            Project00Context context = new Project00Context(options);


    //         //this is an example of dependency injection
    //         //I'm "injecting" an instance of business logic layer to restaurant menu, and an implementation of 
    //         // IRepo to business logic
    //         // IRepo dataLayer = new FileRepo();
    //         // IBL businessLogic = new BL(dataLayer);
    //         // IMenu restaurantMenu = new RestaurantMenu(businessLogic);

            switch (menuString.ToLower()) 
            {
                // case "admin":
                    // return new AdminMenu(new BL(new Repo(context)));
                    case "account":
                    return new CustomerNavigation(new BL(new Repo(context)));
                    case "order menu":
                    return new OrderMenu(new BL(new Repo(context)));
                    
                    
                    
                    
                    //  case
                    // return new OrderMenu(new BL(new Repo(context)));
                    //  case:
                    // return new OrderMenu(new BL(new Repo(context)));
                    default:
                    return null;
            }
        }
    }
}