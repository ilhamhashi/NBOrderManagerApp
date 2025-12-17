namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a product with an id, name, description, and price
/// Also includes a list of Taste and Size objects related to the product.
/// </summary>
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<Taste> TasteOptions { get; set; } = [];
    public List<Size> SizeOptions { get; set; } = [];
    public Taste Taste { get; set; }
    public Size Size { get; set; }

    public Product(int id, string name, string description, decimal price)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
    }

    public Product(string name, string description, decimal price, List<Size> sizes, List<Taste> tastes)
    {
        Name = name;
        Description = description;
        Price = price;
        SizeOptions = sizes;
        TasteOptions = tastes;
    }
}
