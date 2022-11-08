using DishyApi.Configuration;
using Microsoft.Extensions.Options;
using MySqlConnector;

namespace DishyApi.Services;

/// <summary>
/// Service for generating <see cref="MySqlConnection"/> instances.
/// </summary>
public class DbConnService : IDbConnService
{
    private readonly MysqlSettings _settings;

    /// <summary>
    /// Creates a new instance of <see cref="DbConnService"/> with the given set of settings.
    /// </summary>
    /// <param name="settings">The settings for the connection.</param>
    public DbConnService(IOptions<MysqlSettings> settings)
    {
        _settings = settings.Value;
    }

    /// <summary>
    /// Creates a new instance of <see cref="MySqlConnection"/>.
    /// </summary>
    /// <returns>A <see cref="MySqlConnection"/> with the connstring set.</returns>
    public MySqlConnection GetConnection()
    {
        return new MySqlConnection(_settings.ConnectionString);
    }
}
