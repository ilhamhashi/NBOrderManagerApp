using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class OrderLineRepository : IRepository<OrderLine>
{
    private readonly ISqlDataAccess _db;

    public OrderLineRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public void Add(OrderLine entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<OrderLine> GetAll()
    {
        throw new NotImplementedException();
    }

    public OrderLine GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(OrderLine entity)
    {
        throw new NotImplementedException();
    }
}
