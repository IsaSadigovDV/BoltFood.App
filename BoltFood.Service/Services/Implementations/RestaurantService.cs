using BoltFood.Core.Enums;
using BoltFood.Core.Models;
using BoltFood.Core.Repositories.RestaurantRepository;
using BoltFood.Data.Repositories.RestaurantRepository;
using BoltFood.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BoltFood.Service.Services.Implementations
{
    public class RestaurantService : IRestaurantService
    {
        private readonly IRestaurantRepository _restaurantRepository = new restaurantRepository();
        public async Task<string> CreateAsync(string name, RestaurantCategoryEnum restaurantCategoryEnum)
        {
            Restaurant restaurant = new Restaurant(name, restaurantCategoryEnum);
            Console.ForegroundColor= ConsoleColor.Green;
            await _restaurantRepository.AddAsync(restaurant);
            return "Succesfully Created!";
        }

        public async Task<List<Restaurant>> GetAllAsync()
        {
            return await _restaurantRepository.GetAllAsync();
        }

        public async Task<Restaurant> GetAsync(int id)
        {
            Restaurant restaurant = await _restaurantRepository.GetAysnc(x=>x.Id== id);

            if(restaurant == null)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("Restaurant is not found. Please add a restaurant then check again!");
            }   

            Console.ForegroundColor= ConsoleColor.Green;
            return restaurant;
        }

        public async Task<List<Product>> GetProductByRestaurant(string Name)
        {
            Restaurant restaurant = await _restaurantRepository.GetAysnc(x=>x.name== Name);

            if (restaurant == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Restaurant is not found. Please add a restaurant then check again!");
                return null;
            }

            return restaurant.products;
        }

        public async Task<string> RemoveAsync()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter id number to remove");

            int.TryParse(Console.ReadLine(), out int id);
            Restaurant restaurant = await _restaurantRepository.GetAysnc(x=>x.Id == id);

            if (restaurant == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Restaurant is not found.";
            }

            await _restaurantRepository.RemoveAsync(restaurant);
            Console.ForegroundColor = ConsoleColor.Green;
            return "Restaurant is removed succesfully";
            
        }

        public async Task<string> UpdateAsync(int id , string name)
        {
             Restaurant restaurant = await _restaurantRepository.GetAysnc(x=>x.Id == id);
            if (restaurant == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return "Restaurant is not found. Please add a restaurant";
            }
            Console.WriteLine("Enter restaurant name");
            restaurant.name = name;
            Console.ForegroundColor = ConsoleColor.Green;
            return "Restaurant is updated succesfully";

        }

        public Task<string> UpdateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
