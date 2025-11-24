using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;

public class PaymentRepository : IRepository<Payment>
{
    private readonly SqlConnection _connection;

    public PaymentRepository(ISqlDataAccess sqlDataAccess)
    {
        _connection = sqlDataAccess.GetSqlConnection();
    }
    public int Insert(Payment entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Payment entity)
    {
        throw new NotImplementedException();
    }
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Payment> GetAll()
    {
        throw new NotImplementedException();
    }

    public Payment GetById(int id)
    {
        throw new NotImplementedException();
    }
}
