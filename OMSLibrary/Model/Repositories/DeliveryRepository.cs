using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class DeliveryRepository : IRepository<Delivery>
{
    public Task Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Delivery>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<Delivery?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(Delivery entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Delivery entity)
    {
        throw new NotImplementedException();
    }
}
