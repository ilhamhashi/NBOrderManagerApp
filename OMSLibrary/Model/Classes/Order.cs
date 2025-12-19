namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents an order from a customer.
/// </summary>
public class Order
{
    /// <summary>
    /// Represents a unique Id for each order
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Represents the order creation date
    /// </summary>
    public DateTime Date { get; set; }
    /// <summary>
    /// Represent the current order status 
    /// based on an enumeration. 
    /// </summary>
    public OrderStatus Status { get; set; } 
    /// <summary>
    /// Represents the customer who made the order
    /// </summary>
    public Customer? Customer { get; set; }
    /// <summary>
    /// Represents the pickup/delivery event of the order
    /// </summary>
    public PickUp PickUp { get; set; }
    /// <summary>
    /// Represents a note attached to the order
    /// </summary>
    public Note Note { get; set; }
    /// <summary>
    /// Represents the collection of 
    /// orderlines the order is composed of
    /// </summary>
    public List<OrderLine> OrderLines { get; set; }
    /// <summary>
    /// Represents the collection of payments
    /// made on the order. 
    /// </summary>
    public List<Payment> Payments { get; set; }



    /// <summary>
    /// Constructor used for adding a new order when Id is not known. 
    /// </summary>
    public Order(DateTime orderDate, Customer customer, PickUp pickUp, Note note)
    {
        Date = orderDate;
        Customer = customer;
        PickUp = pickUp;
        Note = note;
    }

    /// <summary>
    /// Constructor used when creating an instance 
    /// from the datasource and the Id is known. 
    /// </summary>
    public Order(int id, DateTime orderDate, OrderStatus status, Customer? customer, PickUp pickUp, Note note)
    {
        Id = id;
        Date = orderDate;
        Status = status;
        Customer = customer;
        PickUp = pickUp;
        Note = note;
    }

    /// <summary>
    /// Constructor used when creating an instance
    /// from the datasource and the order is a foreign key
    /// </summary>
    public Order (int id)
    {
        Id = id;
    }
}
