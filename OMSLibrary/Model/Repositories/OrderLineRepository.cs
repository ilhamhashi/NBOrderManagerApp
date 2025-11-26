using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class OrderLineRepository : IRepository<OrderLine>
{
    private readonly SqlConnection _connection;

    public OrderLineRepository(ISqlDataAccess sqlDataAccess)
    {
        _connection = sqlDataAccess.GetSqlConnection();
    }

    public void Delete(params object[] keyValues)
    {
        using SqlCommand command = new SqlCommand("spOrderLine_Delete", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@ProductId", keyValues[0]);
        command.Parameters.AddWithValue("@OrderId", keyValues[1]);
        _connection.Open();
        command.ExecuteNonQuery();
    }

    public IEnumerable<OrderLine> GetAll()
    {
        var orderLines = new List<OrderLine>();
        using SqlCommand command = new("spOrderLine_GetAll", _connection);
        command.CommandType = CommandType.StoredProcedure;
        _connection.Open();

        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            orderLines.Add(new OrderLine
                ((int)reader["ProductId"],
                (int)reader["OrderId"],
                (int)reader["Quantity"],
                (decimal)reader["Price"],
                (decimal)reader["Discount"]));
        }
        return orderLines;
    }

    public OrderLine GetById(params object[] keyValues)
    {
        OrderLine orderLine = null;
        using SqlCommand command = new SqlCommand("spOrderLine_GetById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@ProductId", keyValues[0]);
        command.Parameters.AddWithValue("@OrderId", keyValues[1]);
        _connection.Open();

        using SqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            orderLine = new OrderLine
                ((int)reader["ProductId"],
                (int)reader["OrderId"],
                (int)reader["Quantity"],
                (decimal)reader["Price"],
                (decimal)reader["Discount"]);
        }
        return orderLine;
    }

    public int Insert(OrderLine entity)
    {
        using SqlCommand command = new SqlCommand("spOrderLine_Insert", _connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@ProductId", entity.ProductId);
        command.Parameters.AddWithValue("@OrderId", entity.OrderId);
        command.Parameters.AddWithValue("@Quantity", entity.Quantity);
        command.Parameters.AddWithValue("@Price", entity.Price);
        command.Parameters.AddWithValue("@Discount", entity.Discount);

        _connection.Open();
        command.ExecuteNonQuery();

        return null;
    }

    public void Update(OrderLine entity)
    {
        using SqlCommand command = new SqlCommand("spOrderLine_Update", _connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@ProductId", entity.ProductId);
        command.Parameters.AddWithValue("@OrderId", entity.OrderId);
        command.Parameters.AddWithValue("@Quantity", entity.Quantity);
        command.Parameters.AddWithValue("@Price", entity.Price);
        command.Parameters.AddWithValue("@Discount", entity.Discount);

        _connection.Open();
        command.ExecuteNonQuery();
    }
}
