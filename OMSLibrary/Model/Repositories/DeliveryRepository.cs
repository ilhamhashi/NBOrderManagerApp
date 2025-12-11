using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;

/// <summary>
/// Repository responsible for handling all database operations related to Delivery.
/// Implements CRUD functionality using stored procedures.
/// </summary>
public class DeliveryRepository : IRepository<Delivery>
{
    private readonly IDataAccess _db;

    /// <summary>
    /// Initializes the repository with a database connection provider.
    /// </summary>
    public DeliveryRepository(IDataAccess db)
    {
        _db = db;
    }

    /// <summary>
    /// Inserts a new delivery record into the database.
    /// Returns the generated CollectionId.
    /// </summary>
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
    /// <summary>
    /// Updates an existing delivery entry in the database.
    /// </summary>
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
    /// <summary>
    /// Deletes a delivery record using its CollectionId.
    /// </summary>
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
    /// <summary>
    /// Retrieves a delivery by its CollectionId.
    /// Returns null if not found.
    /// </summary>
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
    /// <summary>
    /// Retrieves all deliveries stored in the database.
    /// </summary>
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

