
using BoltFood.Core.Enums;
using BoltFood.Core.Models;
using BoltFood.Core.Models.Base;

public class Product:BaseModel
{
    private static int _id;
    public double Price { get; set; }
    public ProductCategoryEnum productCategoryEnum { get; set; }
    public Restaurant Restaurant { get; set; }
    public Product(string Name, double price,ProductCategoryEnum category):base(Name)
    {
        _id++;
        Id = _id;
        name = Name;
        Price = price;
        productCategoryEnum = category;
    }

    public override string ToString()
    {
        return $"Product id: {Id}, Product Name: {name}, Product price: {Price}," +
            $"Product category {productCategoryEnum}";
    }

}

