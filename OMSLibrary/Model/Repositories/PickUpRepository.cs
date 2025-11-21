using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;

public class PickUpRepository : IRepository<PickUp>
{
    public Task Delete(int Id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PickUp>> GetAll()
    {
        throw new NotImplementedException();
    }

    public Task<PickUp?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task Insert(PickUp entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(PickUp entity)
    {
        throw new NotImplementedException();
    }
}
