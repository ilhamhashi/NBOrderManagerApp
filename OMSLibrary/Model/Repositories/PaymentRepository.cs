using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;

public class PaymentRepository : IRepository<Payment>
{
    private readonly IDataAccess _db;

    public PaymentRepository(IDataAccess db)
    {
        _db = db;
    }

    public int Insert(Payment entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPayment_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new SqlParameter("@Id", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@Date", entity.Date);
            command.Parameters.AddWithValue("@Amount", entity.Amount);
            command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethod.Id);
            command.Parameters.AddWithValue("@OrderId", entity.Order.Id);
            command.Parameters.Add(outputParam);

            connection.Open();
            command.ExecuteNonQuery();
            return (int)outputParam.Value;
        }
    }

    public void Update(Payment entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPayment_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", entity.Id);
            command.Parameters.AddWithValue("@Date", entity.Date);
            command.Parameters.AddWithValue("@Amount", entity.Amount);
            command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethod.Id);
            command.Parameters.AddWithValue("@OrderId", entity.Order.Id);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPayment_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    public Payment GetById(params object[] keyValues)
    {
        Payment payment = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPayment_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                payment = new Payment
                    ((int)reader["Id"],
                    (DateTime)reader["Date"], 
                    (decimal)reader["Amount"], 
                    new PaymentMethod((int)reader["PaymentMethodId"]),
                    new Order((int)reader["OrderId"]));
            }
            return payment;
        }
    }

    public IEnumerable<Payment> GetAll()
    {
        var payments = new List<Payment>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPayment_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                payments.Add(new Payment
                (
                    (int)reader["Id"],
                    (DateTime)reader["Date"],
                    (decimal)reader["Amount"],
                    new PaymentMethod((int)reader["PaymentMethodId"]),
                    new Order((int)reader["OrderId"])
                ));
            }
            return payments;
        }
    }
}
