using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;

namespace OrderManagerLibrary.Model.Repositories;
public class ProductRepository : IRepository<Product>
{
    private readonly SqlConnection _connection;

    public ProductRepository(ISqlDataAccess sqlDataAccess)
    {
        _connection = sqlDataAccess.GetSqlConnection();
    }
    public int Insert(Product entity)
    {
        throw new NotImplementedException();
    }

    public void Update(Product entity)
    {
        throw new NotImplementedException();
    }
    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Product> GetAll()
    {
        throw new NotImplementedException();
    }

    public Product GetById(int id)
    {
        throw new NotImplementedException();
    }
}
