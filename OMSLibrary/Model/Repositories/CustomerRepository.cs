using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
/// <summary>
/// Repository for handling database operations related to Customer entities.
/// Implements CRUD operations using stored procedures.
/// </summary>
public class CustomerRepository : IRepository<Customer>
{
    private readonly IDataAccess _db;

    /// <summary>
    /// Initializes a new instance of the repository
    /// with access to the application's database connection.
    /// </summary>
    public CustomerRepository(IDataAccess db)
    {
        _db = db;
    }
    /// <summary>
    /// Inserts a new customer into the database.
    /// Returns the newly generated CustomerId.
    /// </summary>
    public int Insert(Customer entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spCustomer_Insert", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            SqlParameter outputParam = new SqlParameter("@CustomerId", SqlDbType.Int);
            outputParam.Direction = ParameterDirection.Output;

            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@LastName", entity.LastName);
            command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
            command.Parameters.Add(outputParam);

            connection.Open();
            command.ExecuteNonQuery();
            return (int)outputParam.Value;
        }
    }
    /// <summary>
    /// Updates an existing customer's information based on CustomerId.
    /// </summary>
    public void Update(Customer entity)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spCustomer_Update", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
            command.Parameters.AddWithValue("@FirstName", entity.FirstName);
            command.Parameters.AddWithValue("@LastName", entity.LastName);
            command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    /// <summary>
    /// Deletes a customer from the database using CustomerId.
    /// </summary>
    public void Delete(params object[] keyValues)
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spCustomer_Delete", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CustomerId", keyValues[0]);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
    /// <summary>
    /// Retrieves a customer by ID from the database.
    /// Returns null if no match is found.
    /// </summary>
    public Customer GetById(params object[] keyValues)
    {
        Customer customer = null;
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spCustomer_GetById", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@CustomerId", keyValues[0]);
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();

            if (reader.Read())
            {
                customer = new Customer
                    ((int)reader["CustomerId"],
                    (string)reader["FirstName"],
                    (string)reader["LastName"],
                    (string)reader["PhoneNumber"]);
            }
            return customer;
        }
    }

    /// <summary>
    /// Retrieves all customers stored in the database.
    /// </summary>
    public IEnumerable<Customer> GetAll()
    {
        var customers = new List<Customer>();
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spCustomer_GetAll", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            using SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                customers.Add(new Customer
                    ((int)reader["CustomerId"],
                    (string)reader["FirstName"],
                    (string)reader["LastName"],
                    (string)reader["PhoneNumber"]));
            }
            return customers;
        }
    }
}
