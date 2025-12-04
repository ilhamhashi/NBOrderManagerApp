namespace OrderManagerLibrary.Model.Classes;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<Taste> Tastes { get; set; }
    public List<Size> Sizes { get; set; }
    public Taste SelectedTaste { get; set; }
    public Size SelectedSize { get; set; }

    public Product(int id, string name, string description, decimal price)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
    }

    public Product(string name, string description, decimal price)
    {
        Name = name;
        Description = description;
        Price = price;
    }
}
