using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;
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
