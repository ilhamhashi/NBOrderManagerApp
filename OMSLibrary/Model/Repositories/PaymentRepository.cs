using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;

public class PaymentRepository : IRepository<Payment>
{
    private readonly SqlConnection _connection;

    public PaymentRepository(IConfiguration config)
    {
        _connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
    }

    public void Delete(params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Payment> GetAll()
    {
        throw new NotImplementedException();
    }

    public Payment GetById(params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public int Insert(Payment entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Payment entity)
    {
        throw new NotImplementedException();
    }
}
