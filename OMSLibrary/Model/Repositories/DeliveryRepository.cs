using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class DeliveryRepository : IRepository<Delivery>
{
    private readonly IDataAccess _db;

    public DeliveryRepository(IDataAccess db)
    {
        _db = db;
    }

    public int Insert(Delivery entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spDelivery_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new SqlParameter("@CollectionId", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@CollectionDate", entity.CollectionDate);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.AddWithValue("@Neighborhood", entity.Neighborhood);
            command.Parameters.Add(outputParam);

            connection.Open();
            command.ExecuteNonQuery();
            return (int)outputParam.Value;
        }
    }

    public void Update(Delivery entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spDelivery_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CollectionId", entity.CollectionId);
            command.Parameters.AddWithValue("@CollectionDate", entity.CollectionDate);
            command.Parameters.AddWithValue("@Neighborhood", entity.Neighborhood);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spDelivery_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CollectionId", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    public Delivery GetById(params object[] keyValues)
    {
        Delivery delivery = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spDelivery_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CollectionId", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                delivery = new Delivery
                    ((int)reader["CollectionId"],
                    (DateTime)reader["CollectionDate"],
                    (int)reader["OrderId"],
                    (string)reader["Neighborhood"]);
            }
        }
        return delivery;
    }

    public IEnumerable<Delivery> GetAll()
    {
        var deliveries = new List<Delivery>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spDelivery_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                deliveries.Add(new Delivery
                (
                    (int)reader["CollectionId"],
                    (DateTime)reader["CollectionDate"],
                    (int)reader["OrderId"],
                    (string)reader["Neighborhood"]
                ));
            }
        }
        return deliveries;
    }
}
