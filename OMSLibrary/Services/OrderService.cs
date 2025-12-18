using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Repositories;
using OrderManagerLibrary.Services.Interfaces;

namespace OrderManagerLibrary.Services;
public class OrderService : IOrderService
{
    private readonly IDataAccess _db;
    private readonly IRepository<Order> _orderRepository;
    private readonly IOrderLineService _orderLineService;
    private readonly IPaymentService _paymentService;
    private readonly IRepository<PickUp> _pickUpRepository;
    private readonly IRepository<Note> _noteRepository;
    private readonly IRepository<Customer> _customerRepository;

    public OrderService(IDataAccess dataAccess, IRepository<Order> orderRepository,
                        IRepository<Note> noteRepository, IRepository<PickUp> pickUpRepository,
                        IRepository<Customer> customerRepository, IOrderLineService orderLineService, 
                        IPaymentService paymentService)
    {
        _db = dataAccess;
        _orderRepository = orderRepository;
        _noteRepository = noteRepository;
        _pickUpRepository = pickUpRepository;
        _customerRepository = customerRepository;
        _orderLineService = orderLineService;
        _paymentService = paymentService;
    }

    public IEnumerable<Order> GetAllOrders()
    {
        var orders = _orderRepository.GetAll();

        foreach (var order in orders)
        {
            order.Customer = _customerRepository.GetById(order.Customer.Id);
            order.PickUp = _pickUpRepository.GetById(order.PickUp.Id);
            order.Note = _noteRepository.GetById(order.Note.Id);
            order.OrderLines = [.. _orderLineService.GetAllOrderLinesByOrder(order)];
            order.Payments = [.. _paymentService.GetAllPayments().Where(p => p.Order.Id == order.Id)];
        }
        return orders;
    }

    public IEnumerable<Order> GetAllOrdersByCustomer(Customer customer)
    {
        var customerOrders = (_orderRepository as OrderRepository).GetByCustomerId(customer.Id);
        foreach (var order in customerOrders)
        {
            order.Customer = customer;
            order.PickUp = _pickUpRepository.GetById(order.PickUp.Id);
            order.Note = _noteRepository.GetById(order.Note.Id);
        }
        return customerOrders;
    }

    public IEnumerable<Order> GetUpcomingOrders()
    {
        var upcomingOrders = (_orderRepository as OrderRepository).GetUpcomingOrders();
        foreach (var order in upcomingOrders)
        {
            order.Customer = _customerRepository.GetById(order.Customer.Id);
            order.PickUp = _pickUpRepository.GetById(order.PickUp.Id);
        }
        return upcomingOrders;
    }
    public IEnumerable<Order> GetPendingPaymentOrders()
    {
        var pendingPaymentOrders = (_orderRepository as OrderRepository).GetPendingPaymentOrders();
        foreach (var order in pendingPaymentOrders)
        {
            order.Customer = _customerRepository.GetById(order.Customer.Id);
            order.PickUp = _pickUpRepository.GetById(order.PickUp.Id);
        }
        return pendingPaymentOrders;
    }

    public Order CreateOrder(Order order, List<OrderLine> orderLines, List<Payment> payments)
    {
        using (SqlConnection connection = _db.GetConnection())
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    order.PickUp.Id = _pickUpRepository.Insert(order.PickUp);
                    order.Note.Id = _noteRepository.Insert(order.Note);
                    order.Id = _orderRepository.Insert(order);

                    foreach (var line in orderLines)
                    {
                        line.Order = order;
                        _orderLineService.CreateOrderLine(line);
                    }

                    foreach (var payment in payments)
                    {
                        payment.Order = order;
                        _paymentService.CreatePayment(payment);
                    }

                    transaction.Commit();

                    return order;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }
}