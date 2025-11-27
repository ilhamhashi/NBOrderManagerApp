namespace OrderManagerLibrary.Model.Classes;
public class OrderLine
{
    public Product Product { get; set; }
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int LineNumber { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal Discount { get; set; } = 0;

    public OrderLine(int productId, int orderId, int lineNumber, int quantity, decimal price, decimal discount)
    {
        ProductId = productId;
        OrderId = orderId;
        LineNumber = lineNumber;        
        Quantity = quantity;
        Price = price;
        Discount = discount;
    }

    public OrderLine(Product product, int lineNumber, int quantity, decimal price, decimal discount)
    {
        Product = product;
        LineNumber = lineNumber;
        Quantity = quantity;
        Price = price;
        Discount = discount;
    }

    public void ReducePrice()
    {
        Price -= Discount;
    }
}
