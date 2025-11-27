using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class DeliveryRepository : IRepository<Delivery>
{
    private readonly SqlConnection _connection;

    public DeliveryRepository(ISqlDataAccess sqlDataAccess)
    {
        _connection = sqlDataAccess.GetSqlConnection();
    }

    public int Insert(Delivery entity)
    {
        using SqlCommand command = new SqlCommand("spDelivery_Insert", _connection);
        command.CommandType = CommandType.StoredProcedure;
        SqlParameter outputParam = new SqlParameter("@CollectionId", SqlDbType.Int);
        outputParam.Direction = ParameterDirection.Output;

        command.Parameters.AddWithValue("@CollectionDate", entity.CollectionDate);
        command.Parameters.AddWithValue("@OrderId", entity.OrderId);
        command.Parameters.AddWithValue("@Neighborhood", entity.Neighborhood);
        command.Parameters.Add(outputParam);

        _connection.Open();
        command.ExecuteNonQuery();
        return (int)outputParam.Value;
    }

    public void Update(Delivery entity)
    {
        using SqlCommand command = new SqlCommand("spDelivery_Update", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@CollectionId", entity.CollectionId);
        command.Parameters.AddWithValue("@CollectionDate", entity.CollectionDate);
        command.Parameters.AddWithValue("@OrderId", entity.OrderId);
        command.Parameters.AddWithValue("@Neighborhood", entity.Neighborhood);
        _connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(int id)
    {
        using SqlCommand command = new SqlCommand("spDelivery_Delete", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@CollectionId", id);
        _connection.Open();
        command.ExecuteNonQuery();
    }
    public Delivery GetById(int id)
    {
        Delivery delivery = null;
        using SqlCommand command = new SqlCommand("spDelivery_GetById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@CollectionId", id);
        _connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            delivery = new Delivery
                ((int)reader["CollectionId"],
                (DateTime)reader["CollectionDate"],
                (int)reader["OrderId"],
                (string)reader["Neighborhood"]);
        }
        return delivery;
    }

    public IEnumerable<Delivery> GetAll()
    {
        var deliveries = new List<Delivery>();
        using SqlCommand command = new("spDelivery_Insert", _connection);
        command.CommandType = CommandType.StoredProcedure;
        _connection.Open();

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
        return deliveries;
    }
}
