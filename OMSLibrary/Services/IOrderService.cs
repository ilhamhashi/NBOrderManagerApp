using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Services
{
    public interface IOrderService
    {
        IEnumerable<Product> ViewProductCatalogue();
        void CreateOrder(Order order, List<OrderLine> orderLines, List<IPaymentMethod> paymentMethods, List<Payment> payments, ICollectionType collection, INote? note);
        void UpdateOrder(Order order);
    }
}