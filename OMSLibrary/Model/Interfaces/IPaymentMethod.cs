namespace OrderManagerLibrary.Model.Interfaces;

public interface IPaymentMethod
{
    int PaymentMethodId { get; set; }
    string Name { get; set; }
}
