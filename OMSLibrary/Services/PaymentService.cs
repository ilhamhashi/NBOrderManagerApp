using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Repositories;
using OrderManagerLibrary.Services.Interfaces;

namespace OrderManagerLibrary.Services;
public class PaymentService : IPaymentService
{
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IRepository<PaymentMethod> _paymentMethodRepository;

    public PaymentService(IRepository<Payment> paymentRepository, IRepository<PaymentMethod> paymentMethodRepository)
    {
        _paymentRepository = paymentRepository;
        _paymentMethodRepository = paymentMethodRepository;
    }

    public IEnumerable<PaymentMethod> GetPaymentMethods()
    {
        return _paymentMethodRepository.GetAll();
    }

    public IEnumerable<Payment> GetAllPayments()
    {
        var payments = _paymentRepository.GetAll();

        foreach (var payment in payments)
        {
            payment.PaymentMethod = _paymentMethodRepository.GetById(payment.PaymentMethod.Id);
        }
        return payments;
    }

    public Payment GetPaymentById(int id) => _paymentRepository.GetById(id);
    public Payment CreatePayment(Payment payment)
    {
        payment.Id = _paymentRepository.Insert(payment);
        return payment;
    }
    public void UpdatePayment(Payment payment) => _paymentRepository.Update(payment);
    public void RemovePayment(int id) => _paymentRepository.Delete(id);
}
