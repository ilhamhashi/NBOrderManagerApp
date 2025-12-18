
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Repositories;
using OrderManagerLibrary.Services.Interfaces;

namespace OrderManagerLibrary.Services;
public class OrderLineService : IOrderLineService
{
    private readonly IRepository<OrderLine> _orderLineRepository;
    private readonly IProductService _productService;

    public OrderLineService(IRepository<OrderLine> orderLineRepository,
                            IProductService productService)
    {
        _orderLineRepository = orderLineRepository;
        _productService = productService;
    }

    public void CreateOrderLine(OrderLine orderLine)
    {
        _orderLineRepository.Insert(orderLine);
    }

    public IEnumerable<OrderLine> GetAllOrderLinesByOrder(Order order)
    {
        var orderLines = _orderLineRepository.GetAll().Where(o => o.Order.Id == order.Id);
        foreach (var line in orderLines)
        {
            line.Order = order;
            line.Product = _productService.GetAllProducts().FirstOrDefault(p => p.Id == line.Product.Id);
        }
        return orderLines;
    }

    public void RemoveOrderLine(int id)
    {
        throw new NotImplementedException();
    }

    public void UpdateOrderLine(OrderLine orderLine)
    {
        throw new NotImplementedException();
    }
}
