using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class PickUpRepository : IRepository<PickUp>
{
    private readonly IDataAccess _db;

    public PickUpRepository(IDataAccess db)
    {
        _db = db;
    }
    public int Insert(PickUp entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPickUp_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new SqlParameter("@CollectionId", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@CollectionDate", entity.CollectionDate);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.Add(outputParam);

            connection.Open();
            command.ExecuteNonQuery();
            return (int)outputParam.Value;
        }
    }

    public void Update(PickUp entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPickUp_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CollectionId", entity.CollectionId);
            command.Parameters.AddWithValue("@CollectionDate", entity.CollectionDate);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPickUp_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CollectionId", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public PickUp GetById(params object[] keyValues)
    {
        PickUp pickUp = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPickUp_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CollectionId", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                pickUp = new PickUp
                    ((int)reader["CollectionId"],
                    (DateTime)reader["CollectionDate"],
                    (int)reader["OrderId"]);
            }
        }
        return pickUp;
    }    
    
    public IEnumerable<PickUp> GetAll()
    {
        var pickUps = new List<PickUp>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPickUp_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                pickUps.Add(new PickUp
                (
                    (int)reader["CollectionId"],
                    (DateTime)reader["CollectionDate"],
                    (int)reader["OrderId"]
                ));
            }
        }
        return pickUps;
    }


}
