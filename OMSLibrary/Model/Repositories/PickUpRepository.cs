using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class PickUpRepository : IRepository<PickUp>
{
    private readonly SqlConnection _connection;

    public PickUpRepository(ISqlDataAccess sqlDataAccess)
    {
        _connection = sqlDataAccess.GetSqlConnection();
    }
    public int Insert(PickUp entity)
    {
        throw new NotImplementedException();
    }
    public void Update(PickUp entity)
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
}
