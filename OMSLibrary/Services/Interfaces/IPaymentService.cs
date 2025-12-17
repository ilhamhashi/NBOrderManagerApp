using OrderManagerLibrary.Model.Classes;

namespace OrderManagerLibrary.Services.Interfaces;
public interface IPaymentService
{
    IEnumerable<PaymentMethod> GetPaymentMethods();
}
