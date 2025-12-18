

namespace OrderManagerLibrary.Model.Classes;

/// <summary>
/// Represents a payment for an order, including the amount, date,
/// and payment method.
/// </summary>
public class Payment
{
    public int Id { get; set; }
    public DateTime Date { get; set; }
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
    public Order Order { get; set; }


    public Payment(int id,  DateTime date, decimal amount, PaymentMethod paymentMethod, Order order)
    {
        Id = id;
        Date = date;
        Amount = amount;
        PaymentMethod = paymentMethod;
        Order = order;
    }

    public Payment(decimal paymentAmount, DateTime paymentDate, PaymentMethod paymentMethod)
    {
        Amount = paymentAmount;
        Date = paymentDate;
        PaymentMethod = paymentMethod;
    }
}
