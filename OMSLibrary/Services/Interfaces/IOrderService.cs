using OrderManagerLibrary.Model.Classes;


namespace OrderManagerLibrary.Services.Interfaces
{
    public interface IOrderService
    {

        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetAllOrdersByCustomer(Customer customer);
        IEnumerable<Order> GetUpcomingOrders();
        IEnumerable<Order> GetPendingPaymentOrders();
        Order CreateOrder(Order order, List<OrderLine> orderLines,List<Payment> payments);
    }
}