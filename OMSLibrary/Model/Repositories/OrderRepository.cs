using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class OrderRepository : IRepository<Order>
{
    private readonly SqlConnection _connection;

    public OrderRepository(ISqlDataAccess sqlDataAccess)
    {
        _connection = sqlDataAccess.GetSqlConnection();
    }

    public int Insert(Order entity)
    {
        using SqlCommand command = new SqlCommand("spOrder_Insert", _connection);
        command.CommandType = CommandType.StoredProcedure;
        SqlParameter outputParam = new SqlParameter("@OrderId", SqlDbType.Int);
        outputParam.Direction = ParameterDirection.Output;

        command.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
        command.Parameters.AddWithValue("@OrderStatus", entity.Status);
        command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
        command.Parameters.Add(outputParam);
        
        _connection.Open();
        command.ExecuteNonQuery();
        return (int)outputParam.Value;
    }

    public void Update(Order entity)
    {
        using SqlCommand command = new SqlCommand("spOrder_Update", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@OrderId", entity.OrderId);
        command.Parameters.AddWithValue("@OrderDate", entity.OrderDate);
        command.Parameters.AddWithValue("@OrderStatus", entity.Status);
        command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
        _connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(params object[] keyValues)
    {
        using SqlCommand command = new SqlCommand("spOrder_Delete", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@OrderId", keyValues[0]);
        _connection.Open();
        command.ExecuteNonQuery();
    }
    public Order GetById(params object[] keyValues)
    {
        Order order = null;
        using SqlCommand command = new SqlCommand("spOrder_GetById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@OrderId", keyValues[0]);
        _connection.Open();

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

    public IEnumerable<Order> GetAll()
    {
        var orders = new List<Order>();
        using SqlCommand command = new("spOrder_GetAll", _connection);
        command.CommandType = CommandType.StoredProcedure;
        _connection.Open();

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

