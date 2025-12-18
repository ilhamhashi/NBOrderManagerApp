using OrderManagerLibrary.Model.Classes;

namespace OrderManagerLibrary.Services.Interfaces;
public interface IPaymentService
{
    Payment GetPaymentById(int id);
    Payment CreatePayment(Payment payment);
    void UpdatePayment(Payment payment);
    void RemovePayment(int id);
    IEnumerable<Payment> GetAllPayments();
    IEnumerable<PaymentMethod> GetPaymentMethods();
}
