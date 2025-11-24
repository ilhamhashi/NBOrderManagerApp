using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class PickUpRepository : IRepository<PickUp>
{
    private readonly ISqlDataAccess _db;

    public PickUpRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public void Add(PickUp entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PickUp> GetAll()
    {
        throw new NotImplementedException();
    }

    public PickUp GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(PickUp entity)
    {
        throw new NotImplementedException();
    }
}
