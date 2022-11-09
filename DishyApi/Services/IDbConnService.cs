using MySqlConnector;

namespace DishyApi.Services;

/// <summary>
/// Interface for services which generating <see cref="MySqlConnection"/> instances.
/// </summary>
public interface IDbConnService
{
    /// <summary>
    /// Creates a new instance of <see cref="MySqlConnection"/>.
    /// </summary>
    /// <returns>A <see cref="MySqlConnection"/> with the connstring set.</returns>
    MySqlConnection GetConnection();
}
