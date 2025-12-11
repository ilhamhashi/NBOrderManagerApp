using Microsoft.Data.SqlClient;
namespace OrderManagerLibrary.DataAccess;
public interface IDataAccess
{
    SqlConnection GetConnection();
}