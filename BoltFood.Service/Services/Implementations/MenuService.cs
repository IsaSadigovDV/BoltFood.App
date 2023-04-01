using BoltFood.Core.Enums;
using BoltFood.Core.Models;
using BoltFood.Data.Repositories.RestaurantRepository;
using BoltFood.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Service.Services.Implementations
{
    public class MenuService : IMenuService
    {
        public void AnimatedWriteline(string message, ConsoleColor color)
        {
            int delay = 20;
            Console.ForegroundColor = color;
            foreach (char c in message)
            {
                Console.Write(c);
                Thread.Sleep(delay);
            }
            Console.ResetColor();
            Console.WriteLine();

        }
        public async Task ShowMenuAsync()
        {
            show();
            int.TryParse(Console.ReadLine(), out int request);    
            while (request!=0)
            {
               switch(request)
                {
                  case 1:
                        Console.Clear();
                        await CreateRestaurant();
                        break;

                    case 2:
                        Console.Clear();
                        await ShowAllRestaurant();
                        break;
                    case 3:
                        Console.Clear();
                        await GetRestaurant();
                        break;
                    case 4:
                        Console.Clear();

                        break;

                }
            }
        }
        /*
        public async Task ShowMenuAsync()
        {

            AnimatedWriteline("Welcome to Bolt Food console application",ConsoleColor.Magenta);
            AnimatedWriteline("App is developed by Isa Sadigov", ConsoleColor.Yellow);
            AnimatedWriteline("Please select one of theese options", ConsoleColor.Yellow);

           
            bool status = true;
            while (status is true)
            {
                AnimatedWriteline("1.Restaurant", ConsoleColor.Magenta);
                AnimatedWriteline("2.Product", ConsoleColor.Cyan);
                AnimatedWriteline("3.About", ConsoleColor.Green);
                int.TryParse(Console.ReadLine(), out int request);
               
                switch (request)
                {
                    case 1:
                        AnimatedWriteline("1.Create a restaurant", ConsoleColor.Magenta);
                        AnimatedWriteline("2.Update a restaurant", ConsoleColor.Magenta);
                        AnimatedWriteline("3.Remove a restaurant" , ConsoleColor.Magenta);
                        AnimatedWriteline("4.Get a restaurant by Id", ConsoleColor.Magenta);
                        AnimatedWriteline("5.Get all restaurants ", ConsoleColor.Magenta);

                        while(status)
                        {
                            int.TryParse(Console.ReadLine(), out request);
                            switch (request)
                            {
                                case 1:
                                    await CreateRestaurant();
                                    break;
                                case 6:
                                    status = false;
                                    break;
                                default:
                                    break;
                            }
                        }
                        status = true;
                        break;
                    case 2:
                        AnimatedWriteline("Add a product", ConsoleColor.Cyan);
                        AnimatedWriteline("Update a product", ConsoleColor.Cyan);
                        AnimatedWriteline("Remove a product", ConsoleColor.Cyan);
                        AnimatedWriteline("Get a product by id", ConsoleColor.Cyan);
                        AnimatedWriteline("Get all products", ConsoleColor.Cyan);
                        break;
                    case 3:
                        AnimatedWriteline("Bolt Food is a food delivery service offered by the transportation network company Bolt (formerly known as Taxify). The service operates in various cities across Europe, " +
                            "Africa, and the Middle East. With Bolt Food, customers can order food from local restaurants and have it delivered directly to their door.\r\n\r\nTo use Bolt Food, customers need to " +
                            "download the Bolt app and select the \"Food Delivery\" " +
                            "option. From there, they can browse through the list of available restaurants and menu items, place an order, and pay for it through the app. Once the order is confirmed," +
                            " the customer can track the delivery in real-time and receive " +
                            "notifications when the driver is on their way" +
                            ".\r\n\r\nOne of the benefits of Bolt Food is that it offers " +
                            "affordable delivery fees, which can be as low as 1 euro in some cities. Bolt Food also claims to offer fast and reliable delivery times, with an average delivery time of 30 " +
                            "minutes or less.\r\n\r\nHowever, as with any food delivery service," +
                            " there are some potential drawbacks to using Bolt Food." +
                            " These may include higher prices for menu items compared to ordering " +
                            "directly from the restaurant, potential issues with food quality or accuracy of orders, and delays or issues with the delivery process.\r\n\r\nOverall," +
                            " Bolt Food can be a convenient option for customers who want to order food from their favorite restaurants without leaving their homes. However" +
                            ", it's important to weigh the pros and cons and make an informed decision based on your individual needs and preferences.", ConsoleColor.Green);
                        break;
                }
            }
        }

        */

        private void show()
        {
            Console.WriteLine("1.Create a restaurant");
            Console.WriteLine("2.Show all restaurant");
            Console.WriteLine("3.Get a restaurant");
            Console.WriteLine("4.Update a restaurant");
            Console.WriteLine("5.Remove a restaurant");
            Console.WriteLine("6.Create a product");
            Console.WriteLine("7.Show all products");
            Console.WriteLine("8.Get a product");
            Console.WriteLine("9.Update a product");
            Console.WriteLine("10.Remove a product");

        }
        private readonly RestaurantService _restaurantservice = new RestaurantService();
        private readonly ProductService _productService = new ProductService();
        private async Task CreateRestaurant()
        {
            Console.WriteLine("Please choose Resstaurant category");

            var Enums = Enum.GetValues(typeof(RestaurantCategoryEnum));

            foreach (var item in Enums)
            {
                //note: (int)item gives us numbers before enum values
                Console.WriteLine((int)item + "." + item);
            }
            int.TryParse(Console.ReadLine(), out int restaurantCategory);

            try
            {
                var restaurantcategory = Enums.GetValue(restaurantCategory - 1);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please choose correct option!!!"); 
            }
            Console.WriteLine("Please add restaurant name");
            string name = Console.ReadLine();
           string message = await _restaurantservice.CreateAsync(name, (RestaurantCategoryEnum)restaurantCategory);
        }


        private async Task ShowAllRestaurant()
        {
            List<Restaurant> restaurants = await _restaurantservice.GetAllAsync();

            foreach (var item in restaurants)
            {
                Console.WriteLine(item);
            }
        }


        private async Task GetRestaurant()
        {
            AnimatedWriteline("Please enter id to get a restaurant", ConsoleColor.Cyan);
            int.TryParse(Console.ReadLine(),out int id);

            Restaurant restaurant = await _restaurantservice.GetAsync(id);
            Console.WriteLine(restaurant);
        }

        private async Task UpdateRestaurant()
        {
            Console.WriteLine("Please enter restaurant id to update");
            int.TryParse(Console.ReadLine(), out int id);

            string message = await _restaurantservice.UpdateAsync();
        }

    }
}
