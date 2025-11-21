using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;
public class Order
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public OrderStatus Status { get; set; }
    public int CustomerId { get; set; }

    public Order(int orderId, DateTime orderDate, OrderStatus status, int customerId)
    {
        OrderId = orderId;
        OrderDate = orderDate;
        Status = status;
        CustomerId = customerId;
    }

    public Order(DateTime orderDate, OrderStatus status, int customerId)
    {
        OrderDate = orderDate;
        Status = status;
        CustomerId = customerId;
    }
}
