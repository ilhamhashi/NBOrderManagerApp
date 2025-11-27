using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccessNS;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class MobilePaymentRepository : IRepository<MobilePayment>
{
    private readonly IDataAccess _db;

    public MobilePaymentRepository(IDataAccess db)
    {
        _db = db;
    }
    public int Insert(MobilePayment entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spMobilePayment_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new("@PaymentMethodId", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@Name", entity.Name);
            command.Parameters.Add(outputParam);
            connection.Open();
            command.ExecuteNonQuery();

            return (int)outputParam.Value;
        }
    }

    public void Update(MobilePayment entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spMobilePayment_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
            command.Parameters.AddWithValue("@Name", entity.Name);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spMobilePayment_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PaymentMethodId", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    public MobilePayment GetById(params object[] keyValues)
    {
        MobilePayment mobilePayment = null;

        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spMobilePayment_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@PaymentMethodId", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                mobilePayment = new MobilePayment(
                    (int)reader["PaymentMethodId"],
                    (string)reader["Name"]
                );
            }
            return mobilePayment;
        }
    }


    public IEnumerable<MobilePayment> GetAll()
    {
        var mobilePayments = new List<MobilePayment>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spMobilePayment_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                mobilePayments.Add(new MobilePayment(
                    (int)reader["PaymentMethodId"],
                    (string)reader["Name"]
                ));
            }
            return mobilePayments;
        }
    }
}
