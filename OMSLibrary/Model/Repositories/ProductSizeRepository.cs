using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class ProductSizeRepository : IRepository<ProductSize>
{
    private readonly IDataAccess _db;

    public ProductSizeRepository(IDataAccess db)
    {
        _db = db;
    }
    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spProductSize_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SizeId", keyValues[0]);
            command.Parameters.AddWithValue("@ProductId", keyValues[1]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public IEnumerable<ProductSize> GetAll()
    {
        var pSizes = new List<ProductSize>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spProductSize_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                pSizes.Add(new ProductSize
                    ((int)reader["SizeId"],
                    (int)reader["ProductId"]));
            }
            return pSizes;
        }
    }

    public ProductSize GetById(params object[] keyValues)
    {
        ProductSize pSize = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spProductSize_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SizeId", keyValues[0]);
            command.Parameters.AddWithValue("@ProductId", keyValues[1]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                pSize = new ProductSize
                    ((int)reader["SizeId"],
                    (int)reader["ProductId"]);
            }
            return pSize;
        }
    }

    public int Insert(ProductSize entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spProductSize_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SizeId", entity.SizeId);
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            connection.Open();
            command.ExecuteNonQuery();
            return -1;
        }
    }

    public void Update(ProductSize entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spProductSize_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SizeId", entity.SizeId);
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
