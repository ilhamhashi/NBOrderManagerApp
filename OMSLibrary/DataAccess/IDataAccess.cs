using Microsoft.Data.SqlClient;

namespace OrderManagerLibrary.DataAccessNS;
public interface IDataAccess
{
    SqlConnection GetConnection();
}