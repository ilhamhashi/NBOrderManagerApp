using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class ProductTasteRepository : IRepository<ProductTaste>
{
    private readonly IDataAccess _db;

    public ProductTasteRepository(IDataAccess db)
    {
        _db = db;
    }

    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spProductTaste_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TasteId", keyValues[0]);
            command.Parameters.AddWithValue("@ProductId", keyValues[1]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public IEnumerable<ProductTaste> GetAll()
    {
        var pTastes = new List<ProductTaste>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spProductTaste_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                pTastes.Add(new ProductTaste
                    ((int)reader["TasteId"],
                    (int)reader["ProductId"]));
            }
            return pTastes;
        }
    }

    public ProductTaste GetById(params object[] keyValues)
    {
        ProductTaste pTaste = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spProductTaste_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TasteId", keyValues[0]);
            command.Parameters.AddWithValue("@ProductId", keyValues[1]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                pTaste = new ProductTaste
                    ((int)reader["TasteId"],
                    (int)reader["ProductId"]);
            }
            return pTaste;
        }
    }

    public int Insert(ProductTaste entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spProductTaste_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TasteId", entity.TasteId);
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            connection.Open();
            command.ExecuteNonQuery();
            return -1;
        }
    }

    public void Update(ProductTaste entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spProductTaste_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@TasteId", entity.TasteId);
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
