namespace OrderManagerLibrary.Model.Classes;
public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ImagePath { get; set; }

    public Product(int productId, string productName, string description, decimal price, string imagePath)
    {
        ProductId = productId;
        ProductName = productName;
        Description = description;
        Price = price;
        ImagePath = imagePath;
    }
}
