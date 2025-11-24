using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Classes;
public class MobilePaymentMethod : IPaymentMethod
{
    public int PaymentMethodId { get; set; }
    public string Name { get; set; }

    public MobilePaymentMethod(int paymentMethodId, string name)
    {
        PaymentMethodId = paymentMethodId;
        Name = name;
    }
}    
