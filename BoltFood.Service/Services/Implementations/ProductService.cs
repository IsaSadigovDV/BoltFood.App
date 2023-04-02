using BoltFood.Core.Enums;
using BoltFood.Core.Models;
using BoltFood.Core.Repositories.RestaurantRepository;
using BoltFood.Data.Repositories.RestaurantRepository;
using BoltFood.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Service.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IRestaurantRepository _restaurantRepository = new restaurantRepository();

        public async Task<string> CreateAsync(int restoranId, string name, double price, ProductCategoryEnum category)
        {

            Restaurant restaurant =  await _restaurantRepository.GetAysnc(x => x.Id == restoranId);
            if (restaurant == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Res not have");
                return null;
            }

            Product product = new Product(restaurant,name,price,category);
            restaurant.productsList.Add(product);
            Console.ForegroundColor= ConsoleColor.Green;
            return "Product is created succesfully";
            
        }

        public async Task<List<Product>> GetAllAsync()
        {
            List<Restaurant> restaurants = await _restaurantRepository.GetAllAsync();

            List<Product> products = new List<Product>();
            foreach (var item in restaurants)
            {
                products.AddRange(item.productsList);
            }
            return products;
        }

        public async Task<Product> GetAsync(int id)
        {
            List<Restaurant> restaurants = await _restaurantRepository.GetAllAsync();

            foreach (var item in restaurants)
            {
                Product product = item.productsList.Find(x => x.Id == id);
                if (product !=null)
                {
                    return product;
                }
            }
            return null;
        }

        

        public async Task<string> RemoveAsync(int id)
        {
            List<Restaurant> restaurants = await _restaurantRepository.GetAllAsync();

            foreach (var item in restaurants)
            {
                Product product = item.productsList.Find(x => x.Id == id);
                if (product != null)
                {
                    item.productsList.Remove(product);
                    await _restaurantRepository.UpdateAsync(item);
                    Console.ForegroundColor = ConsoleColor.Green;
                    return "Product is removed succesfully";
                }
            }
            Console.ForegroundColor = ConsoleColor.Red; 
            return "Product is not found. Please try again!";
        }

        public async Task<string> UpdateAsync(int id, string name, double price)
        {
            List<Restaurant> restaurants = await _restaurantRepository.GetAllAsync();

            foreach (var item in restaurants)
            {
                Product product = item.productsList.Find(x => x.Id == id);
                if (product != null)
                {

                    Console.WriteLine("Please enter product name");
                    string Name = Console.ReadLine();

                    Console.WriteLine("Please enter product price");
                    double.TryParse(Console.ReadLine(), out  price);

                    Console.WriteLine("Please select category of product");
                    // category readline could be
                    var Enums = Enum.GetValues(typeof(ProductCategoryEnum));
                    foreach (var productenum in Enums)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine((int)productenum + "." + productenum);
                    }
                    int.TryParse(Console.ReadLine(), out int productcategory);

                    try
                    {
                        Enums.GetValue(productcategory);
                    }
                    catch (Exception)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        return "You must enter product category correctly";

                    }
                    Restaurant restaurant = await _restaurantRepository.GetAysnc(x => x.Id == id);
                    product.Price = price;
                    product.name = Name;
                    product.productCategoryEnum = (ProductCategoryEnum)productcategory;
                    product.UpdatedDate = DateTime.Now;
                    await _restaurantRepository.UpdateAsync(item);
                    Console.ForegroundColor = ConsoleColor.Green;
                    return "Product is removed succesfully";
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            return "Product is not found. Please try again!";
        }

    
    }
}
