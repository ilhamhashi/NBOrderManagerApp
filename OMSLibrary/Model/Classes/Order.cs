using OrderManagerLibrary.Model.Interfaces;
namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents an order from a customer.
/// </summary>
public class Order
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public OrderStatus Status { get; set; }
    public int CustomerId { get; set; }
    public int PickUpId { get; set; }
    public int NoteId { get; set; }    
    
    public Customer Customer { get; set; }
    public PickUp PickUp { get; set; }
    public Note Note { get; set; }
    public List<OrderLine> OrderLines { get; set; }
    public List<Payment> Payments { get; set; }



    public Order(int id, DateTime date, OrderStatus status, int customerId, int pickUpId, int noteId)
    {
        Id = id;
        Date = date;
        Status = status;
        CustomerId = customerId;
        PickUpId = pickUpId;
        NoteId = noteId;
    }

    /// <summary>
    /// Constructor used when retrieving an existing order from the database.
    /// OrderId is already known and assigned by the database.
    /// </summary>
    public Order(int orderId, DateTime orderDate, OrderStatus status, int customerId)
    {
        Id = orderId;
        Date = orderDate;
        Status = status;
        CustomerId = customerId;
    }

    // <summary>
    /// Constructor used when creating a new order before saving it to the database.
    /// OrderId will be generated automatically when inserted.
    /// </summary>

    public Order(DateTime orderDate, OrderStatus status, Customer customer, PickUp pickUp, Note note)
    {
        Date = orderDate;
        Status = status;
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
}
