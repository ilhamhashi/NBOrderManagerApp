using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccessNS;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class OrderLineRepository : IRepository<OrderLine>
{
    private readonly IDataAccess _db;

    public OrderLineRepository(IDataAccess db)
    {
        _db = db;
    }
    public int Insert(OrderLine entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrderLine_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.AddWithValue("@LineNumber", entity.LineNumber);
            command.Parameters.AddWithValue("@Quantity", entity.Quantity);
            command.Parameters.AddWithValue("@Price", entity.Price);
            command.Parameters.AddWithValue("@Discount", entity.Discount);
            connection.Open();
            command.ExecuteNonQuery();
            return -1;
        }
    }
    public void Update(OrderLine entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrderLine_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductId", entity.ProductId);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.AddWithValue("@LineNumber", entity.LineNumber);
            command.Parameters.AddWithValue("@Quantity", entity.Quantity);
            command.Parameters.AddWithValue("@Price", entity.Price);
            command.Parameters.AddWithValue("@Discount", entity.Discount);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrderLine_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductId", keyValues[0]);
            command.Parameters.AddWithValue("@OrderId", keyValues[1]);
            command.Parameters.AddWithValue("@LineNumber", keyValues[2]);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public OrderLine GetById(params object[] keyValues)
    {
        OrderLine orderLine = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrderLine_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@ProductId", keyValues[0]);
            command.Parameters.AddWithValue("@OrderId", keyValues[1]);
            command.Parameters.AddWithValue("@LineNumber", keyValues[2]);

            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                orderLine = new OrderLine
                    ((int)reader["ProductId"],
                    (int)reader["OrderId"],
                    (int)reader["LineNumber"],
                    (int)reader["Quantity"],
                    (decimal)reader["Price"],
                    (decimal)reader["Discount"]);
            }
            return orderLine;
        }
    }

    public IEnumerable<OrderLine> GetAll()
    {
        var orderLines = new List<OrderLine>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrderLine_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderLines.Add(new OrderLine
                    ((int)reader["ProductId"],
                    (int)reader["OrderId"],
                    (int)reader["LineNumber"],
                    (int)reader["Quantity"],
                    (decimal)reader["Price"],
                    (decimal)reader["Discount"]));
            }
            return orderLines;
        }
    }
}
