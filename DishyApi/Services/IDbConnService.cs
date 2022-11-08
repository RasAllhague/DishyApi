using MySqlConnector;

namespace DishyApi.Services;

public interface IDbConnService
{
    MySqlConnection GetConnection();
}
