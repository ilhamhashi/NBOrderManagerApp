using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace OrderManagerLibrary.DataAccess;

public class SqlDataAccess : ISqlDataAccess
{
    private readonly IConfigurationRoot _config;
    // TODO add "new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();" to DI

    public SqlDataAccess(IConfigurationRoot config)
    {
        _config = config;
    }
}
