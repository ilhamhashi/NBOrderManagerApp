namespace OrderManagerLibrary.Model.Classes;
public class OrderLine
{
    public int ProductId { get; set; }
    public int OrderId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public bool IsCustomProduct { get; set; }

    public OrderLine(int productId, int orderId, int quantity, decimal price, bool isCustomProduct)
    {
        ProductId = productId;
        OrderId = orderId;
        Quantity = quantity;
        Price = price;
        IsCustomProduct = isCustomProduct;
    }
}
