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
        public RestaurantCategoryEnum RestaurantCategoryEnum { get; set; }

        public List<Product> products;
        public Product Product { get; set; }

        public Restaurant(string name,RestaurantCategoryEnum restaurantCategory):base(name)
        {
            _id++;
            Id = _id;
            products= new List<Product>();
            RestaurantCategoryEnum = restaurantCategory;
        }
    }
}
