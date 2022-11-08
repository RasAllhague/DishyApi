using DishyApi.Models.User;

namespace DishyApi.Services;

public readonly record struct UserEdit(string email, string? username, string? password);

public interface IUserService
{
    Task<bool> VerifyLoginAsync(string email, string password);
    Task LogoutAsync(UserModel user);

    Task<UserModel?> GetUserAsync(int id);
    Task<UserModel?> GetUserAsync(string email);
    Task<IEnumerable<UserModel>> GetUsersAsync();
    Task<bool> DeleteUserAsync(int id);
    Task<UserModel?> CreateUserAsync(UserModel user);
    Task<UserModel?> UpdateUserAsync(UserEdit user);
    Task<bool> ExistsAsync(int id);
    Task<bool> ExistsAsync(string email);
}
