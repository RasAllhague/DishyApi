using MySqlConnector;

namespace DishyApi.Services;

/// <summary>
/// Service for generating <see cref="MySqlConnection"/> instances.
/// </summary>
public class DbConnService : IDbConnService
{
    private readonly string _connString;

    /// <summary>
    /// Creates a new instance of <see cref="DbConnService"/> with the given db connection string.
    /// </summary>
    /// <param name="connString">The connection string which should be used by this service.</param>
    public DbConnService(string connString)
    {
        _connString = connString;
    }

    /// <summary>
    /// Creates a new instance of <see cref="MySqlConnection"/>.
    /// </summary>
    /// <returns>A <see cref="MySqlConnection"/> with the connstring set.</returns>
    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_connString);
    }
}
