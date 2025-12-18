using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
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
            SqlParameter outputParam = new SqlParameter("@Id", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@Date", entity.Date);
            command.Parameters.AddWithValue("@Status", entity.Status);
            command.Parameters.AddWithValue("@CustomerId", entity.Customer.Id);
            command.Parameters.AddWithValue("@PickUpId", entity.PickUp.Id);
            command.Parameters.AddWithValue("@NoteId", entity.Note.Id);
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
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Date", entity.Date);
            command.Parameters.AddWithValue("@Status", entity.Status);
            command.Parameters.AddWithValue("@CustomerId", entity.Customer.Id);
            command.Parameters.AddWithValue("@PickUpId", entity.PickUp.Id);
            command.Parameters.AddWithValue("@NoteId", entity.Note.Id);
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
            command.Parameters.AddWithValue("@Id", keyValues[0]);
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
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                order = new Order
                    ((int)reader["Id"],
                    (DateTime)reader["Date"],
                    (OrderStatus)Enum.Parse(typeof(OrderStatus), (string)reader["Status"]),
                    new Customer((int)reader["CustomerId"]),
                    new PickUp((int)reader["PickUpId"]),
                    new Note((int)reader["NoteId"]));
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
                    (int)reader["Id"],
                    (DateTime)reader["Date"],
                    (OrderStatus)Enum.Parse(typeof(OrderStatus), (string)reader["Status"]),
                    new Customer((int)reader["CustomerId"]),
                    new PickUp((int)reader["PickUpId"]),
                    new Note((int)reader["NoteId"])
                ));
            }
            return orders;
        }
    }

    public IEnumerable<Order> GetUpcomingOrders()
    {
        var upcomingOrders = new List<Order>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_GetUpcomingOrders", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                upcomingOrders.Add(new Order
                (
                    (int)reader["Id"],
                    (DateTime)reader["Date"],
                    (OrderStatus)Enum.Parse(typeof(OrderStatus), (string)reader["Status"]),
                    new Customer((int)reader["CustomerId"]),
                    new PickUp((int)reader["PickUpId"]),
                    new Note((int)reader["NoteId"])
                ));
            }
            return upcomingOrders;
        }
    }
    public IEnumerable<Order> GetPendingPaymentOrders()
    {
        var pendingPaymentOrders = new List<Order>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_GetPendingPaymentOrders", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                pendingPaymentOrders.Add(new Order
                (
                    (int)reader["Id"],
                    (DateTime)reader["Date"],
                    (OrderStatus)Enum.Parse(typeof(OrderStatus), (string)reader["Status"]),
                    new Customer((int)reader["CustomerId"]),
                    new PickUp((int)reader["PickUpId"]),
                    new Note((int)reader["NoteId"])
                ));
            }
            return pendingPaymentOrders;
        }
    }

    public IEnumerable<Order> GetByCustomerId(int id)
    {
        var orders = new List<Order>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spOrder_GetAllByCustomerId", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CustomerId", id);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                orders.Add(new Order
                (
                    (int)reader["Id"],
                    (DateTime)reader["Date"],
                    (OrderStatus)Enum.Parse(typeof(OrderStatus), (string)reader["Status"]),
                    new Customer((int)reader["CustomerId"]),
                    new PickUp((int)reader["PickUpId"]),
                    new Note((int)reader["NoteId"])
                ));
            }
            return orders;
        }
    }
}

