using MySqlConnector;

namespace DishyApi.Services;

public class DbConnService : IDbConnService
{
    private readonly string _connString;

    public DbConnService(string connString)
    {
        _connString = connString;
    }

    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connString);
    }
}
