using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccessNS;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class OrderRepository : IRepository<Order>
{
    private readonly IDataAccess _db;

    public OrderRepository(IDataAccess db)
    {
        _db = db;
    }

    public int Insert(Order entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new SqlParameter("@OrderId", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
            command.Parameters.AddWithValue("@OrderStatus", entity.Status);
            command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
            command.Parameters.Add(outputParam);

            connection.Open();
            command.ExecuteNonQuery();
            return (int)outputParam.Value;
        }
    }

    public void Update(Order entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
            command.Parameters.AddWithValue("@OrderStatus", entity.Status);
            command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@OrderId", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    public Order GetById(params object[] keyValues)
    {
        Order order = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@OrderId", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                order = new Order
                    ((int)reader["OrderId"],
                    (DateTime)reader["OrderDate"],
                    (OrderStatus)Enum.Parse(typeof(OrderStatus), (string)reader["OrderStatus"]),
                    (int)reader["CustomerId"]);
            }
            return order;
        }
    }

    public IEnumerable<Order> GetAll()
    {
        var orders = new List<Order>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new Order
                (
                    (int)reader["OrderId"],
                    (DateTime)reader["OrderDate"],
                    (OrderStatus)Enum.Parse(typeof(OrderStatus), (string)reader["OrderStatus"]),
                    (int)reader["CustomerId"]
                ));
            }
            return orders;
        }
    }
}

