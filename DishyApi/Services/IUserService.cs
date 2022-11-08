using DishyApi.Models.User;

namespace DishyApi.Services;

/// <summary>
/// A record struct which is only for user edit informations.
/// </summary>
/// <param name="email">The mail of the user to edit.</param>
/// <param name="username">The new username or null.</param>
/// <param name="password">The new password or null.</param>
public readonly record struct UserEdit(string email, string? username, string? password);

/// <summary>
/// A interface for services which work with users.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Verifies the login information of a user.
    /// </summary>
    /// <param name="email">The email address of the user to check.</param>
    /// <param name="password">The password of the user to check.</param>
    /// <returns>true if successfull or false if failed.</returns>
    Task<bool> VerifyLoginAsync(string email, string password);
    /// <summary>
    /// Logs the user out and disables the token of the user.
    /// </summary>
    /// <param name="user">The user to logout.</param>
    Task LogoutAsync(UserModel user);

    /// <summary>
    /// Gets a user by his id.
    /// </summary>
    /// <param name="id">The id of the user to get.</param>
    /// <returns>The found <see cref="UserModel"/> instance or if no user with this id was found null.</returns>
    Task<UserModel?> GetUserAsync(int id);
    /// <summary>
    /// Gets a user by his email address.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <returns>The found <see cref="UserModel"/> instance or if no user with this id was found null.</returns>
    Task<UserModel?> GetUserAsync(string email);
    /// <summary>
    /// Gets all users in the database.
    /// </summary>
    /// <returns>A <see cref="IEnumerable{T}"/> containing <see cref="UserModel"/>.</returns>
    Task<IEnumerable<UserModel>> GetUsersAsync();
    /// <summary>
    /// Deletes an user with a given id from the database.
    /// </summary>
    /// <param name="id">The id of the user to delete.</param>
    /// <returns>True if successfull false, if not found or no rights for this operation.</returns>
    Task<bool> DeleteUserAsync(int id);
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="user">The data with which the user should be created.</param>
    /// <returns>A new <see cref="UserModel"/> instance if successfull or null if it already exists.</returns>
    Task<UserModel?> CreateUserAsync(UserModel user);
    /// <summary>
    /// Updates a user with given data.
    /// </summary>
    /// <param name="user">The data with which the user should be updated with.</param>
    /// <returns>An updated <see cref="UserModel"/> or null if no user where found.</returns>
    Task<UserModel?> UpdateUserAsync(UserEdit user);
    /// <summary>
    /// Checks whether a user with a given id exists.
    /// </summary>
    /// <param name="id">The id of the user.</param>
    /// <returns>True if user exists, else false.</returns>
    Task<bool> ExistsAsync(int id);
    /// <summary>
    /// Checks whether a user with a given email address exists.
    /// </summary>
    /// <param name="email">The email address of the user.</param>
    /// <returns>True if user exists, else false.</returns>
    Task<bool> ExistsAsync(string email);
}
