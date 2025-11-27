using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class PickUpRepository : IRepository<PickUp>
{
    private readonly SqlConnection _connection;

    public PickUpRepository(IConfiguration config)
    {
        _connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
    }
    public void Delete(params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<PickUp> GetAll()
    {
        throw new NotImplementedException();
    }

    public PickUp GetById(params object[] keyValues)
    {
        throw new NotImplementedException();
    }

    public int Insert(PickUp entity)
    {
        throw new NotImplementedException();
    }

    public void Update(PickUp entity)
    {
        throw new NotImplementedException();
    }
}
