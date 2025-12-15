using OrderManagerLibrary.Model.Interfaces;

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
    public int OrderId { get; set; }
    public int PaymentMethodId { get; set; }

    public IPaymentMethod PaymentMethod { get; set; }
    public Order Order { get; set; }


    public Payment(int id,  DateTime date, decimal amount, int paymentMethodId, int orderId)
    {
        Id = id;
        Date = date;
        Amount = amount;
        PaymentMethod.Id = paymentMethodId;
        Order.Id = orderId;
    }

    public Payment(decimal paymentAmount, DateTime paymentDate, IPaymentMethod paymentMethod)
    {
        Amount = paymentAmount;
        Date = paymentDate;
        PaymentMethod = paymentMethod;
    }
}
