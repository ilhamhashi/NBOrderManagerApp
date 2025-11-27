using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.DataAccessNS;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
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
            SqlParameter outputParam = new SqlParameter("@PaymentId", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@PaymentDate", entity.PaymentDate);
            command.Parameters.AddWithValue("@PaymentAmount", entity.PaymentAmount);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
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
            command.Parameters.AddWithValue("@PaymentId", entity.OrderId);
            command.Parameters.AddWithValue("@PaymentDate", entity.PaymentDate);
            command.Parameters.AddWithValue("@PaymentAmount", entity.PaymentAmount);
            command.Parameters.AddWithValue("@OrderId", entity.OrderId);
            command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
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
            command.Parameters.AddWithValue("@PaymentId", keyValues[0]);
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
            command.Parameters.AddWithValue("@PaymentId", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                payment = new Payment
                    ((int)reader["PaymentId"],
                    (decimal)reader["PaymentAmount"],
                    (DateTime)reader["PaymentDate"], 
                    (int)reader["OrderId"],
                    (int)reader["PaymentMethodId"]);
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
                    (int)reader["PaymentId"],
                    (decimal)reader["PaymentAmount"],
                    (DateTime)reader["PaymentDate"],
                    (int)reader["OrderId"],
                    (int)reader["PaymentMethodId"]
                ));
            }
            return payments;
        }
    }
}
