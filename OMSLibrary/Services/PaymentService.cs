using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
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
}
