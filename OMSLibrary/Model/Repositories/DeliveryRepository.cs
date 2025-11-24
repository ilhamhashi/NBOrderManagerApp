using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class DeliveryRepository : IRepository<Delivery>
{
    private readonly SqlConnection _connection;

    public DeliveryRepository(ISqlDataAccess sqlDataAccess)
    {
        _connection = sqlDataAccess.GetSqlConnection();
    }
    public int Insert(Delivery entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Delivery entity)
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
}
