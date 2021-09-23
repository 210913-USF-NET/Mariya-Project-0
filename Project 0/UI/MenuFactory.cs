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
            //this is an example of dependency injection
            //I'm "injecting" an instance of business logic layer to restaurant menu, and an implementation of 
            // IRepo to business logic
            // IRepo dataLayer = new FileRepo();
            // IBL businessLogic = new BL(dataLayer);
            // IMenu restaurantMenu = new RestaurantMenu(businessLogic);

            // restaurantMenu.Start();
            switch (menuString.ToLower())
            {
                // case "main":
                //     return new MainMenu();
                // case "login":
                //     return new LogInMenu(new BL(new DBRepo(context)), new RestaurantService());
                // case "review":
                //     return new ReviewMenu(new BL(new DBRepo(context)), new RestaurantService());
                // default:
                    return null;
            }
    }
}