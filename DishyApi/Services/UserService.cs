using Dapper;
using DishyApi.Models.User;
using Microsoft.AspNetCore.Identity;
using MySqlConnector;

namespace DishyApi.Services;

public class UserService : IUserService
{
    private readonly IDbConnService _dbConnService;
    private readonly IPasswordHasher<UserModel> _passwordHasher;
    private readonly ILogger<UserService> _logger;

    public UserService(IDbConnService dbConnService, IPasswordHasher<UserModel> passwordHasher, ILogger<UserService> logger)
    {
        _dbConnService = dbConnService;
        _passwordHasher = passwordHasher;
        _logger = logger;
    }

    public async Task<UserModel?> CreateUserAsync(UserModel user)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();
        string insertSql = "INSERT INTO Users (UserName, Email, Password, CreateDate) VALUES (@userName, @email, @password, @createDate);";

        string passwordHash = _passwordHasher.HashPassword(user, user.Password);
        user.Password = passwordHash;

        await conn.ExecuteAsync(insertSql, user);

        return await GetUserAsync(user.Email);
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();
        string sql = "DELETE FROM Users WHERE Id = @id;";

        int affectedRows = await conn.ExecuteAsync(sql, new { id });

        return affectedRows > 0;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        return !((await GetUserAsync(id)) is null);
    }

    public async Task<bool> ExistsAsync(string email)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        return !((await GetUserAsync(email)) is null);
    }

    public async Task<UserModel?> GetUserAsync(int id)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        return await conn.QueryFirstOrDefaultAsync<UserModel>("SELECT * FROM Users WHERE Id = @id;", new { id });
    }

    public async Task<UserModel?> GetUserAsync(string email)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        return await conn.QueryFirstOrDefaultAsync<UserModel>("SELECT * FROM Users WHERE Email = @email;", new { email });
    }

    public async Task<IEnumerable<UserModel>> GetUsersAsync()
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        return await conn.QueryAsync<UserModel>("SELECT * FROM Users;");
    }

    public Task LogoutAsync(UserModel user)
    {
        throw new NotImplementedException();
    }

    public async Task<UserModel?> UpdateUserAsync(UserEdit user)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        if (string.IsNullOrEmpty(user.username) && string.IsNullOrEmpty(user.password))
        {
            return null;
        }

        var dbUser = await GetUserAsync(user.email);

        if (dbUser is null)
        {
            return null;
        }

        string sql = BuildUpdateSql(user);

        int affectedRows = await conn.ExecuteAsync(sql, new
        {
            email = user.email,
            username = user.username,
            password = _passwordHasher.HashPassword(dbUser, user.password)
        });

        if (affectedRows > 1)
        {
            _logger.LogWarning("There have been multiple row updates for the user with email '{0}'.", user.email);
        }

        return await GetUserAsync(user.email);
    }

    private static string BuildUpdateSql(UserEdit user)
    {
        string sql = "UPDATE Users SET ";

        if (!string.IsNullOrEmpty(user.username) && !string.IsNullOrEmpty(user.password))
        {
            sql += " UserName = @username, Password = @password";
        }
        else if (!string.IsNullOrEmpty(user.username))
        {
            sql += " UserName = @username";
        }
        else if (!string.IsNullOrEmpty(user.password))
        {
            sql += " Password = @password";
        }

        sql += " WHERE Email = @email;";
        return sql;
    }

    public Task<bool> VerifyLoginAsync(string email, string password)
    {
        throw new NotImplementedException();
    }
}
