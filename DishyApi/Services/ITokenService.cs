using DishyApi.Models.User;

namespace DishyApi.Services;

public interface ITokenService
{
    string CreateToken(UserModel userModel);
    Task<UserModel?> RetrievedUserFromTokenAsync(HttpContext context);
}
