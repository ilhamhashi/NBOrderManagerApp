using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class CustomerRepository : IRepository<Customer>
{
    private readonly SqlConnection _connection;

    public CustomerRepository(IConfiguration config)
    {
        _connection = new SqlConnection(config.GetConnectionString("DefaultConnection"));
    }

    public int Insert(Customer entity)
    {
        using SqlCommand command = new SqlCommand("spCustomer_Insert", _connection);
        command.CommandType = CommandType.StoredProcedure;
        SqlParameter outputParam = new SqlParameter("@CustomerId", SqlDbType.Int);
        outputParam.Direction = ParameterDirection.Output;

        command.Parameters.AddWithValue("@FirstName", entity.FirstName);
        command.Parameters.AddWithValue("@LastName", entity.LastName);
        command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
        command.Parameters.Add(outputParam);

        _connection.Open();
        command.ExecuteNonQuery();
        return (int)outputParam.Value;
    }

    public void Update(Customer entity)
    {
        using SqlCommand command = new SqlCommand("spCustomer_Update", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@CustomerId", entity.CustomerId);
        command.Parameters.AddWithValue("@FirstName", entity.FirstName);
        command.Parameters.AddWithValue("@LastName", entity.LastName);
        command.Parameters.AddWithValue("@PhoneNumber", entity.PhoneNumber);
        _connection.Open();
        command.ExecuteNonQuery();
    }

    public void Delete(params object[] keyValues)
    {
        using SqlCommand command = new SqlCommand("spCustomer_Delete", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@CustomerId", keyValues[0]);
        _connection.Open();
        command.ExecuteNonQuery();
    }
    public Customer GetById(params object[] keyValues)
    {
        Customer customer = null;
        using SqlCommand command = new SqlCommand("spCustomer_GetById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@CustomerId", keyValues[0]);
        _connection.Open();

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

    public IEnumerable<Customer> GetAll()
    {
        var customers = new List<Customer>();
        using SqlCommand command = new("spCustomer_Insert", _connection);
        command.CommandType = CommandType.StoredProcedure;
        _connection.Open();

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
