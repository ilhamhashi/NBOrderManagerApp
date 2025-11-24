using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Interfaces;
using OrderManagerLibrary.Model.Classes;

namespace OrderManagerLibrary.Model.Repositories;

internal class OrderRepository : IRepository<Order>
{
    private readonly ISqlDataAccess _db;

    public OrderRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public void Add(Order entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Order> GetAll()
    {
        throw new NotImplementedException();
    }

    public Order GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Order entity)
    {
        throw new NotImplementedException();
    }
}

