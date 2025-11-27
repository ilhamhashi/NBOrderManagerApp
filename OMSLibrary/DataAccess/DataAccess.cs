using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OrderManagerLibrary.DataAccessNS;
public class DataAccess : IDataAccess
{
    private readonly IConfiguration _config;

    public DataAccess(IConfiguration config)
    {
        _config = config;
    }

    public SqlConnection GetConnection()
    {
        return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    }
}
