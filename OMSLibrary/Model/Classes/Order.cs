
namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents an order from a customer.
/// </summary>
public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }    
    public Customer Customer { get; set; }
    public PickUp PickUp { get; set; }
    public Note Note { get; set; }
    public List<OrderLine> OrderLines { get; set; }
    public List<Payment> Payments { get; set; }

    public Order(DateTime orderDate, Customer customer, PickUp pickUp, Note note)
    {
        Date = orderDate;
        Customer = customer;
        PickUp = pickUp;
        Note = note;
    }

    public Order(int orderId, DateTime orderDate, OrderStatus status)
    {
        Id = orderId;
        Date = orderDate;
        Status = status;
    }

    public Order(int orderId, string firstName, string lastName, decimal outstandingAmount)
    {
        Id = orderId;
        Customer.FirstName = firstName;
        Customer.LastName = lastName;
    }

    public Order(int id, DateTime orderDate, OrderStatus status, Customer customer, PickUp pickUp, Note note)
    {
        Id = id;
        Date = orderDate;
        Status = status;
        Customer = customer;
        PickUp = pickUp;
        Note = note;
    }

    public Order (int id)
    {
        Id = id;
    }
}
