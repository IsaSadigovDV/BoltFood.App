using BoltFood.Core.Enums;
using BoltFood.Core.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoltFood.Core.Models
{
    public class Restaurant:BaseModel
    {
        private static int _id;
        public RestaurantCategoryEnum RestaurantCategoryEnum { get; set;}

        public List<Product> productsList;
        public Product Product { get; set; }

        public Restaurant(string Name,RestaurantCategoryEnum restaurantCategory):base(Name)
        {
            _id++;
            Id = _id;
            productsList= new List<Product>();
            RestaurantCategoryEnum = restaurantCategory;
            name = Name;
        }
    }
}
