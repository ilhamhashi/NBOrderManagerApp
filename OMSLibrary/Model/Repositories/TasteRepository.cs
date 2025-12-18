using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class TasteRepository : IRepository<Taste>
{
    private readonly IDataAccess _db;

    public TasteRepository(IDataAccess db)
    {
        _db = db;
    }
    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spTaste_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public IEnumerable<Taste> GetAll()
    {
        var tastes = new List<Taste>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spTaste_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tastes.Add(new Taste
                    ((int)reader["Id"],
                    (string)reader["Name"]));
            }
            return tastes;
        }
    }

    public Taste GetById(params object[] keyValues)
    {
        Taste taste = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spTaste_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                taste = new Taste
                    ((int)reader["Id"],
                    (string)reader["Name"]);
            }
            return taste;
        }
    }

    public int Insert(Taste entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spTaste_Insert", connection))
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

    public void Update(Taste entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spTaste_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Name", entity.Name);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public IEnumerable<Taste> GetByProductId(int id)
    {
        var tastes = new List<Taste>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spTaste_GetByProductId", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductId", id);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                tastes.Add(new Taste
                    ((int)reader["Id"],
                    (string)reader["Name"]));
            }
            return tastes;
        }
    }
}
