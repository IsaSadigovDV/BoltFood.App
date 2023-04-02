using BoltFood.Core.Enums;
using BoltFood.Core.Models;
using BoltFood.Data.Repositories.RestaurantRepository;
using BoltFood.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Service.Services.Implementations
{
    public class MenuService : IMenuService
    {
        private readonly IRestaurantService _restaurantservice = new RestaurantService();

        private readonly IProductService _productService = new ProductService();
        public void AnimatedWriteline(string message, ConsoleColor color)
        {
            int delay = 1;
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

            AnimatedWriteline("Welcome to Bolt Food console application", ConsoleColor.Magenta);
            AnimatedWriteline("App is developed by Isa Sadigov", ConsoleColor.Magenta);
            AnimatedWriteline("Please select one of theese options", ConsoleColor.Yellow);

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
                        await UpdateRestaurant();
                        break;
                    case 5:
                        Console.Clear();
                        await RemoveRestaurant();
                        break;
                    case 6:
                        Console.Clear();
                        await CreateProduct();
                        break;
                    case 7:
                        Console.Clear();
                        await ShowAllProducts();
                        break;
                    case 8:
                        Console.Clear();
                        await GetProduct();
                        break;
                    case 9:
                        Console.Clear();
                        await UpdateProduct();
                        break;
                    case 10:
                        Console.Clear();    
                        await RemoveProduct();
                        break;

                    default:
                        Console.Clear();
                        AnimatedWriteline("Please select option number correctly",ConsoleColor.Red);
                        break;
                }

                 Console.ForegroundColor= ConsoleColor.White;
                 show();
                 int.TryParse(Console.ReadLine(), out  request);
            }
        }
        private void show()
        {
            AnimatedWriteline("1.Create a restaurant", ConsoleColor.Yellow);
            AnimatedWriteline("2.Show all restaurant", ConsoleColor.Yellow);
            AnimatedWriteline("3.Get a restaurant", ConsoleColor.Yellow);
            AnimatedWriteline("4.Update a restaurant", ConsoleColor.Yellow);
            AnimatedWriteline("5.Remove a restaurant", ConsoleColor.Yellow);
            AnimatedWriteline("-----------------------", ConsoleColor.Yellow);
            AnimatedWriteline("6.Create a product", ConsoleColor.Yellow);
            AnimatedWriteline("7.Show all products", ConsoleColor.Yellow);
            AnimatedWriteline("8.Get a product", ConsoleColor.Yellow);
            AnimatedWriteline("9.Update a product", ConsoleColor.Yellow);
            AnimatedWriteline("10.Remove a product", ConsoleColor.Yellow);

        }

       
        private async Task CreateRestaurant()
        {
            Console.WriteLine("Please add restaurant name");
            string name = Console.ReadLine();

           if(string.IsNullOrWhiteSpace(name))
            {
                AnimatedWriteline("Name can not be space!!!", ConsoleColor.Red);
                return;
            }


            Console.WriteLine("Please choose Restaurant category");

            var Enums = Enum.GetValues(typeof(RestaurantCategoryEnum));

            foreach (var item in Enums)
            {
                //note: (int)item gives us numbers before enum values
                Console.WriteLine((int)item + "." + item);
            }
            int.TryParse(Console.ReadLine(), out int restaurantCategory);

            try
            {
                Enums.GetValue(restaurantCategory - 1);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please choose correct option!!!");
                return; 
            }

            

           string message =  await _restaurantservice.CreateAsync(name, (RestaurantCategoryEnum)restaurantCategory);
            Console.WriteLine(message);
        }


        private async Task ShowAllRestaurant()
        {
            List<Restaurant> restaurants = await _restaurantservice.GetAllAsync();

            foreach (var item in restaurants)
            {
                AnimatedWriteline($"Restaurant ID {item.Id} Restaurant Name:{item.name} " +
                    $"Restaurant Category{item.RestaurantCategoryEnum}:",ConsoleColor.Green);
            }
        }


        private async Task GetRestaurant()
        {
            AnimatedWriteline("Please enter id to get a restaurant", ConsoleColor.Cyan);
            int.TryParse(Console.ReadLine(),out int id);

            Restaurant restaurant = await _restaurantservice.GetAsync(id);
            Console.WriteLine($"Restaurant ID {restaurant.Id} Restaurant Name: {restaurant.name} " +
                $"Restaurant Category{restaurant.RestaurantCategoryEnum}");
        }

        private async Task UpdateRestaurant()
        {
            Console.WriteLine("Please add a restaurant id to update");
            int.TryParse(Console.ReadLine(), out int id);

            Console.WriteLine("Please enter Restoran");
            string name =  Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Please Add name");
                return;
                
            }


            string message =  await _restaurantservice.UpdateAsync(id, name);
            Console.WriteLine(message);
        }

        private async Task RemoveRestaurant()
        {
            AnimatedWriteline("Enter id number to remove",ConsoleColor.DarkGreen);

            int.TryParse(Console.ReadLine(), out int id);

            string message = await _restaurantservice.RemoveAsync(id);
            Console.WriteLine(message);
        }

        //-----------------------------------------------
        private async Task CreateProduct()
        {
            Console.WriteLine("Please enter Restoran Id");
            int restoranId=int.Parse(Console.ReadLine());

            Console.WriteLine("Please enter product name");
            string name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Please Add name");
                return;

            }

            Console.WriteLine("Please enter product price");
            double.TryParse(Console.ReadLine(), out double price);

            Console.WriteLine("Please select category of product");
            // category readline could be
            var Enums = Enum.GetValues(typeof(ProductCategoryEnum));
            foreach (var item in Enums)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine((int)item + "." + item);
            }
            int.TryParse(Console.ReadLine(), out int productcategory);

            try
            {
                Enums.GetValue(productcategory - 1);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You must enter product category correctly");
            }



            string message = await _productService.CreateAsync(restoranId,name, price, (ProductCategoryEnum)productcategory);
            Console.WriteLine(message);
        }

        private async Task ShowAllProducts()
        {
            List<Product> products = await _productService.GetAllAsync();
         
            foreach (Product product in products)
            {
                Console.WriteLine($"ProductId: {product.Id} ProductName: {product.name} RestoranName:{product.Restaurant.name} ");
            }
        }

        private async Task GetProduct()
        {
            AnimatedWriteline("Please enter product id", ConsoleColor.Magenta);
            int.TryParse(Console.ReadLine(), out int id);

            Product product = await _productService.GetAsync(id);
            Console.WriteLine($"ProductName: {product.name} RestoranName: ");
        }

        private async Task UpdateProduct()
        {
            Console.WriteLine("Please enter product name");
            string name = Console.ReadLine();

            Console.WriteLine("Please enter product price");
            double.TryParse(Console.ReadLine(), out double price);

            Console.WriteLine("Please select category of product");
            // category readline could be
            var Enums = Enum.GetValues(typeof(ProductCategoryEnum));
            foreach (var item in Enums)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine((int)item + "." + item);
            }
            int.TryParse(Console.ReadLine(), out int productcategory);

            try
            {
                Enums.GetValue(productcategory);
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You must enter product category correctly");
            }


            //string message = await _productService.UpdateAsync(id,name,price);
        }

        private async Task RemoveProduct()
        {
            AnimatedWriteline("Please enter product id to delete", ConsoleColor.Cyan);

            int.TryParse(Console.ReadLine(), out int id);

            string message = await _productService.RemoveAsync(id);
            Console.WriteLine(message);
        }
    }
}
