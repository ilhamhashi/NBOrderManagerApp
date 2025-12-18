using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
/// <summary>
/// Repository responsible for handling database operations related to MobilePayment.
/// Provides CRUD functionality using stored procedures.
/// </summary>
public class PaymentMethodRepository : IRepository<PaymentMethod>
{
    private readonly IDataAccess _db;

    /// <summary>
    /// Initializes the repository with access to the database connection provider.
    /// Uses dependency injection for improved testability. 
    /// </summary>
    public PaymentMethodRepository(IDataAccess db)
    {
        _db = db;
    }

    /// <summary>
    /// Inserts a new mobile payment method 
    /// into the database and returns the generated PaymentMethodId.
    /// </summary>
    public int Insert(PaymentMethod entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPaymentMethod_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new("@Id", SqlDbType.Int);
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
    public void Update(PaymentMethod entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPaymentMethod_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", entity.Id);
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
        using (SqlCommand command = new SqlCommand("spPaymentMethod_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }

    /// <summary>
    /// Retrieves a mobile payment method by PaymentMethodId.
    /// Returns null if no matching record is found.
    /// </summary>
    public PaymentMethod GetById(params object[] keyValues)
    {
        PaymentMethod paymentMethod = null;

        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPaymentMethod_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Id", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                paymentMethod = new PaymentMethod(
                    (int)reader["Id"],
                    (string)reader["Name"]
                );
            }
            return paymentMethod;
        }
    }

    /// <summary>
    /// Retrieves all available mobile payment methods from the database.
    /// </summary>
    public IEnumerable<PaymentMethod> GetAll()
    {
        var paymentMethods = new List<PaymentMethod>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spPaymentMethod_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            using SqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                paymentMethods.Add(new PaymentMethod(
                    (int)reader["Id"],
                    (string)reader["Name"]
                ));
            }
            return paymentMethods;
        }
    }
}
