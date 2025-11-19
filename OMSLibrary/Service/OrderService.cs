using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OMSDesktopUI.Services;

public class OrderService
{
    
    private readonly IDbConnection _connection;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<OrderLine> _orderLineRepository;

    public OrderService(IDbConnection connection, IRepository<Order> orderRepository, IRepository<OrderLine> orderLineRepository)
    {
        _connection = connection;
        _orderRepository = orderRepository;
        _orderLineRepository = orderLineRepository;
    }

    public void CreateOrder(Order order, List<OrderLine> orderLines)
    {
        using (var transaction = _connection.BeginTransaction())
        {
            try
            {
                // Opret ordre
                _orderRepository.Insert(order);

                // Opret ordrelinjer
                foreach (var line in orderLines)
                {
                    line.OrderId = order.OrderId; // Sæt OrderId for ordrelinjen
                    _orderLineRepository.Insert(line);
                }

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

    */
}
