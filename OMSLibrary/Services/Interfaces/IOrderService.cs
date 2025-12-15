using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Services.Interfaces
{
    public interface IOrderService
    {

        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrdersByCustomer(Customer customer);
        IEnumerable<Order> GetUpcomingOrders();
        IEnumerable<Order> GetPendingPaymentOrders();
        void CreateOrder(Order order, List<OrderLine> orderLines,List<Payment> payments);
    }
}