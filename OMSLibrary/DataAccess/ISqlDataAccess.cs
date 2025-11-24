using Microsoft.Data.SqlClient;

namespace OrderManagerLibrary.DataAccess;
public interface ISqlDataAccess
{
    SqlConnection GetSqlConnection();
}