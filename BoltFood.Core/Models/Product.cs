
using BoltFood.Core.Enums;
using BoltFood.Core.Models;
using BoltFood.Core.Models.Base;

public class Product:BaseModel
{
    private static int _id;
    public double Price { get; set; }
    public ProductCategoryEnum productCategoryEnum { get; set; }

    public Product(string name, double price,ProductCategoryEnum category):base(name)
    {
        _id++;
        Id = _id;
        Price = price;
        productCategoryEnum = category;
    }

}

