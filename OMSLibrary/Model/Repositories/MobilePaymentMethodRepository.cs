using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class MobilePaymentMethodRepository : IRepository<MobilePaymentMethod>
{
    private readonly ISqlDataAccess _db;

    public MobilePaymentMethodRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public void Add(MobilePaymentMethod entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MobilePaymentMethod> GetAll()
    {
        throw new NotImplementedException();
    }

    public MobilePaymentMethod GetById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(MobilePaymentMethod entity)
    {
        throw new NotImplementedException();
    }
}
