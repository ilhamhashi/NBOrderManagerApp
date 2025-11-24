using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class DeliveryRepository : IRepository<Delivery>
{
    private readonly ISqlDataAccess _db;

    public DeliveryRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public void Add(Delivery entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Delivery> GetAll()
    {
        throw new NotImplementedException();
    }

    public Delivery GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(Delivery entity)
    {
        throw new NotImplementedException();
    }
}
