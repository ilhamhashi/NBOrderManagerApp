using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Repositories;

namespace OrderManagerLibrary.Services;
public class OrderService : IOrderService
{
    private readonly IDataAccess _db;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<OrderLine> _orderLineRepository;
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IRepository<Delivery> _collectionRepository;
    private readonly IRepository<Note> _noteRepository;

    public OrderService(IDataAccess dataAccess, IRepository<Order> orderRepository,
                        IRepository<OrderLine> orderLineRepository,  IRepository<Payment> paymentRepository,
                        IRepository<Delivery> collectionRepository, IRepository<Note> noteRepository, IRepository<Product> productRepository)
    {
        _db = dataAccess;
        _orderRepository = orderRepository;
        _orderLineRepository = orderLineRepository;
        _paymentRepository = paymentRepository;
        _collectionRepository = collectionRepository;
        _noteRepository = noteRepository;
        _productRepository = productRepository;
    }

    public void CreateOrder(Order order, List<OrderLine> orderLines,
                            List<IPaymentMethod> paymentMethods, List<Payment> payments,
                            ICollectionType collection, INote? note)
    {
        using (var transaction = _db.GetConnection().BeginTransaction())
        {
            try
            {
                int i = 0;
                // Opret ordre
                _orderRepository.Insert(order);

                // Opret ordrelinjer
                foreach (var line in orderLines)
                {
                    line.OrderId = order.OrderId; // Sæt OrderId for ordrelinjen
                    _orderLineRepository.Insert(line);
                }

                foreach (var payment in payments)
                {
                    payment.OrderId = order.OrderId;
                    payment.PaymentMethodId = paymentMethods[i].PaymentMethodId;
                    _paymentRepository.Insert(payment);
                    i++;
                }

                collection.OrderId = order.OrderId;
                //_collectionRepository.Insert(collection);

                note.OrderId = note.OrderId;
                //_noteRepository.Insert(note);

                // Commit transaction
                transaction.Commit();
            }
            catch (Exception)
            {
                // Rul tilbage transaktionen ved fejl
                transaction.Rollback();
                throw; // Re-throw exception to handle it upstream
            }
        }
    }
    public IEnumerable<Product> ViewProductCatalogue()
    {
        return _productRepository.GetAll();
    }

    // Method: UpdateOrder
    // Description: Updates an existing order in the repository
    public void UpdateOrder(Order order)
    {
        if (order != null)
        {
            // Fetch the existing order from the database
            var existingOrder = _orderRepository.GetById(order.OrderId);
            if (existingOrder != null)
            {
                // Update the properties of the existing order
                existingOrder.OrderDate = order.OrderDate;
                existingOrder.Status = order.Status;
                existingOrder.CustomerId = order.CustomerId;

                // Save the updated order back to the repository
                _orderRepository.Update(existingOrder);
            }
        }
    }
}