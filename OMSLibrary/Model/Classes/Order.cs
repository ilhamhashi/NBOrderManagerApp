namespace OrderManagerLibrary.Model.Classes;
public class Order : IOrder
{
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public bool IsDraft { get; set; }
    public int CustomerId { get; set; }

    public Order(int orderId, DateTime orderDate, bool isDraft, int customerId)
    {
        OrderId = orderId;
        OrderDate = orderDate;
        IsDraft = isDraft;
        CustomerId = customerId;
    }
}
