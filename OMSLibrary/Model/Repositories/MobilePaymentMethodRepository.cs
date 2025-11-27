using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using OrderManagerLibrary.Model.Classes;
using OrderManagerLibrary.Model.Interfaces;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class MobilePaymentMethodRepository : IRepository<MobilePaymentMethod>
{
    private readonly SqlConnection _connection;

    public MobilePaymentMethodRepository(ISqlDataAccess sqlDataAccess)
    {
        _connection = sqlDataAccess.GetSqlConnection();
    }
    public int Insert(MobilePaymentMethod entity)
    {
        using SqlCommand command = new SqlCommand("spMobilePayment_Insert", _connection);
        command.CommandType = CommandType.StoredProcedure;

        SqlParameter outputParam = new("@PaymentMethodId", SqlDbType.Int);
        outputParam.Direction = ParameterDirection.Output;


        command.Parameters.AddWithValue("@Name", entity.Name);
        command.Parameters.Add(outputParam);

        _connection.Open();
        command.ExecuteNonQuery();
        

        return (int)outputParam.Value;
    }


    public void Update(MobilePaymentMethod entity)
    {
        using SqlCommand command = new SqlCommand("spMobilePayment_Update", _connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@PaymentMethodId", entity.PaymentMethodId);
        command.Parameters.AddWithValue("@Name", entity.Name);

        _connection.Open();
        command.ExecuteNonQuery();
        
    }
    public void Delete(int id)
    {
        using SqlCommand command = new SqlCommand("spMobilePayment_Delete", _connection);
        command.CommandType = CommandType.StoredProcedure;

        command.Parameters.AddWithValue("@PaymentMethodId", id);

        _connection.Open();
        command.ExecuteNonQuery();
        
    }
    public MobilePaymentMethod GetById(int id)
    {
        MobilePaymentMethod mobilePayment = null;

        using SqlCommand command = new SqlCommand("spMobilePayment_GetById", _connection);
        command.CommandType = CommandType.StoredProcedure;
        command.Parameters.AddWithValue("@PaymentMethodId", id);

        _connection.Open();

        using SqlDataReader reader = command.ExecuteReader();
        if (reader.Read())
        {
            mobilePayment = new MobilePaymentMethod(
                (int)reader["PaymentMethodId"],
                (string)reader["Name"]
            );
        }


        return mobilePayment;
    }


    public IEnumerable<MobilePaymentMethod> GetAll()
    {
        var mobilePayments = new List<MobilePaymentMethod>();

        using SqlCommand command = new("spMobilePayment_GetAll", _connection);
        command.CommandType = CommandType.StoredProcedure;

        _connection.Open();
        using SqlDataReader reader = command.ExecuteReader();

        while (reader.Read())
        {
            mobilePayments.Add(new MobilePaymentMethod(
                (int)reader["PaymentMethodId"],
                (string)reader["Name"]
            ));
        }

       
        return mobilePayments;
    }
}


