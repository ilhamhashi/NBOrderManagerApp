using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class SizeRepository : IRepository<Size>
{
    private readonly IDataAccess _db;

    public SizeRepository(IDataAccess db)
    {
        _db = db;
    }

    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spSize_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public IEnumerable<Size> GetAll()
    {
        var sizes = new List<Size>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spSize_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                sizes.Add(new Size
                    ((int)reader["Id"],
                    (string)reader["Name"]));
            }
            return sizes;
        }
    }

    public Size GetById(params object[] keyValues)
    {
        Size size = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spSize_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                size = new Size
                    ((int)reader["Id"],
                    (string)reader["Name"]);
            }
            return size;
        }
    }

    public int Insert(Size entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spSize_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new SqlParameter("@Id", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.Add(outputParam);

            connection.Open();
            command.ExecuteNonQuery();
            return (int)outputParam.Value;
        }
    }

    public void Update(Size entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spSize_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Name", entity.Name);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public IEnumerable<Size> GetByProductId(int id)
    {
        var sizes = new List<Size>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spSize_GetByProductId", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductId", id);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                sizes.Add(new Size
                    (
                        (int)reader["Id"],
                        (string)reader["Name"]
                    ));
            }
            return sizes;
        }
    }
}
