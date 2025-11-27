using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class ProductRepository : IRepository<Product>
{
    private readonly SqlConnection _connection;

    public ProductRepository(IConfiguration config)
    {
        _connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
    }

    public void Delete(params object[] keyValues)
    {
        using SqlCommand command = new SqlCommand("spProduct_Delete", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@ProductId", keyValues[0]);
        _connection.Open();
        command.ExecuteNonQuery();
    }

    public IEnumerable<Product> GetAll()
    {
        var products = new List<Product>();
        using SqlCommand command = new("spProduct_GetAll", _connection);
        command.CommandType = CommandType.StoredProcedure;
        _connection.Open();

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            products.Add(new Product
                ((int)reader["ProductId"],
                (string)reader["Name"],
                (string)reader["Description"],
                (decimal)reader["Price"]));
        }
        return products;
    }

    public Product GetById(params object[] keyValues)
    {
        Product product = null;
        using SqlCommand command = new SqlCommand("spProduct_GetById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@ProductId", keyValues[0]);
        _connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            product = new Product
                ((int)reader["ProductId"],
                (string)reader["Name"],
                (string)reader["Description"],
                (decimal)reader["Price"]);
        }
        return product;
    }

    public int Insert(Product entity)
    {
        using SqlCommand command = new SqlCommand("spProduct_Insert", _connection);
        command.CommandType = CommandType.StoredProcedure;
        SqlParameter outputParam = new SqlParameter("@ProductId", SqlDbType.Int);
        outputParam.Direction = ParameterDirection.Output;

        command.Parameters.AddWithValue("@Name", entity.Name);
        command.Parameters.AddWithValue("@Description", entity.Description);
        command.Parameters.AddWithValue("@Price", entity.Price);
        command.Parameters.Add(outputParam);

        _connection.Open();
        command.ExecuteNonQuery();
        return (int)outputParam.Value;
    }

    public void Update(Product entity)
    {
        using SqlCommand command = new SqlCommand("spProduct_Update", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@ProductId", entity.ProductId);
        command.Parameters.AddWithValue("@Name", entity.Name);
        command.Parameters.AddWithValue("@Description", entity.Description);
        command.Parameters.AddWithValue("@Price", entity.Price);
        _connection.Open();
        command.ExecuteNonQuery();
    }
}
