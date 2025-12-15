namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a single line in an order, including the product,
/// quantity, price, and any discount applied
/// </summary>
public class OrderLine
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int LineNumber { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public string Size { get; set; }
    public string Taste { get; set; }

    public Product Product { get; set; }
    public Order Order { get; set; }

    /// <summary>
    /// Creates an order line using a product ID and order details
    /// </summary>
    public OrderLine(int productId, int orderId, int lineNumber, int quantity, decimal price, decimal discount, string size, string taste)
    {
        ProductId = productId;
        OrderId = orderId;
        LineNumber = lineNumber;        
        Quantity = quantity;
        Price = price;
        Discount = discount;
        Size = size;
        Taste = taste;
    }

    public OrderLine(Product product, int quantity, decimal price, decimal discount, string taste, string size)
    {
        Product = product;
        Quantity = quantity;
        Price = price;
        Discount = discount;
        Taste = taste;
        Size = size;
    }

    /// <summary>
    /// Reduces the price by the discount amount 
    /// </summary>
    public void ApplyDiscount()
    {
        Price -= Discount;
    }

    /// <summary>
    /// Increases the price by the paramenter given amount 
    /// </summary>
    public void IncreasePrice(decimal increaseAmount)
    {
        Price += increaseAmount;
    }

    /// <summary>
    /// Increases orderline quantity by 1 
    /// </summary>
    public void IncreaseQuantity()
    {
        Quantity++;
    }

    /// <summary>
    /// Reduces orderline quantity by 1 
    /// </summary>
    public void DecreaseQuantity()
    {
        Quantity--;
    }
}
