using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace OrderManagerLibrary.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfigurationRoot _config;
        // DI: new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

    public SqlDataAccess(IConfigurationRoot config)
    {
        _config = config;
    }

    public SqlConnection GetSqlConnection()
    {
        return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
    }
}
