using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class MobilePaymentMethodRepository : IRepository<MobilePaymentMethod>
{
    private readonly SqlConnection _connection;

    public MobilePaymentMethodRepository(IConfiguration config)
    {
        _connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
    }
    public void Delete(params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MobilePaymentMethod> GetAll()
    {
        throw new NotImplementedException();
    }

    public MobilePaymentMethod GetById(params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public int Insert(MobilePaymentMethod entity)
    {
        throw new NotImplementedException();
    }

    public void Update(MobilePaymentMethod entity)
    {
        throw new NotImplementedException();
    }
}
