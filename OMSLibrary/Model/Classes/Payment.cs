namespace OrderManagerLibrary.Model.Classes;
public class Payment
{
    public int PaymentId { get; set; }
    public decimal PaymentAmount { get; set; }
    public DateTime PaymentDate { get; set; }
    public int OrderId { get; set; }
    public int PaymentMethodId { get; set; }

    public Payment(int paymentId, decimal paymentAmount, DateTime paymentDate, int orderId, int paymentMethodId)
    {
        PaymentId = paymentId;
        PaymentAmount = paymentAmount;
        PaymentDate = paymentDate;
        OrderId = orderId;
        PaymentMethodId = paymentMethodId;
    }

    public Payment(decimal paymentAmount, DateTime paymentDate, int paymentMethodId)
    {
        PaymentAmount = paymentAmount;
        PaymentDate = paymentDate;
        PaymentMethodId = paymentMethodId;
    }
}
