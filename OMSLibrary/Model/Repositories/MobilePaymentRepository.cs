using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;

/// <summary>
/// Repository responsible for handling database operations related to MobilePayment.
/// Provides CRUD functionality using stored procedures.
/// </summary>
public class MobilePaymentRepository : IRepository<MobilePayment>
{
    private readonly IDataAccess _db;

    /// <summary>
    /// Initializes the repository with access to the database connection provider.
    /// Uses dependency injection for improved testability. 
    /// </summary>
    public MobilePaymentRepository(IDataAccess db)
    {
        _db = db;
    }
    /// <summary>
    /// Inserts a new mobile payment method 
    /// into the database and returns the generated PaymentMethodId.
    /// </summary>
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
    /// <summary>
    /// Updates the name of an existing mobile payment method based on PaymentMethodId.
    /// </summary>
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
    /// <summary>
    /// Deletes a mobile payment method from the database using PaymentMethodId.
    /// </summary>
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
    /// <summary>
    /// Retrieves a mobile payment method by PaymentMethodId.
    /// Returns null if no matching record is found.
    /// </summary>
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

    /// <summary>
    /// Retrieves all available mobile payment methods from the database.
    /// </summary>
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
