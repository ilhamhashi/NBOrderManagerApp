using Microsoft.Data.SqlClient;
using OrderManagerLibrary.DataAccess;
using System.Data;

namespace OrderManagerLibrary.Model.Repositories;
public class SalesDataRepository : ISalesDataRepository
{
    private readonly IDataAccess _db;

    public SalesDataRepository(IDataAccess db)
    {
        _db = db;
    }

    public decimal GetWeeklyRevenue()
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spSale_GetWeeklyRevenue", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            return (decimal)command.ExecuteScalar();
        }
    }

    public decimal GetMonthlyRevenue()
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spSale_GetMonthlyRevenue", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            return (decimal)command.ExecuteScalar();
        }
    }

    public int GetMonthlyOrdersCount()
    {
        using SqlConnection connection = _db.GetConnection();
        using (SqlCommand command = new SqlCommand("spSale_GetMonthlyOrdersCount", connection))
        {
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();

            return (int)command.ExecuteScalar();
        }
    }
}
