using OrderManagerLibrary.Model.Classes;

namespace OrderManagerLibrary.Services.Interfaces;

public interface IOrderLineService
{
    IEnumerable<OrderLine> GetAllOrderLinesByOrder(Order order);
    void CreateOrderLine(OrderLine orderLine);
    void UpdateOrderLine(OrderLine orderLine);
    void RemoveOrderLine(int id);
}
