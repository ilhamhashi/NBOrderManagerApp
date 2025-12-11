using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;
/// <summary>
/// Represents a specific mobile payment method
/// used to process payments for an order.
/// </summary>
public class MobilePayment : IPaymentMethod
{
    public int PaymentMethodId { get; set; }
    public string Name { get; set; }

    public MobilePayment(int paymentMethodId, string name)
    {
        PaymentMethodId = paymentMethodId;
        Name = name;
    }
}
