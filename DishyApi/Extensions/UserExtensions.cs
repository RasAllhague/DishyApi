using DishyApi.Controllers;
using DishyApi.Models.User;
using DishyApi.Services;
using System.Runtime.CompilerServices;

namespace DishyApi.Extensions;

/// <summary>
/// Extension methods for user models classes and records.
/// </summary>
public static class UserExtensions
{
    /// <summary>
    /// Converts an <see cref="UserRequest"/> to an <see cref="UserModel"/>.
    /// </summary>
    /// <param name="request">The request to convert.</param>
    /// <returns>A <see cref="UserModel"/> based on the request with <see cref="UserModel.Id"/> set to 0.</returns>
    public static UserModel ToUserModel(this UserRequest request)
    {
        return new UserModel()
        {
            Id = 0,
            UserName = request.username,
            Email = request.email,
            Password = request.password,
            CreateDate = request.createDate,
            ModifyDate = request.modifyDate
        };
    }

    /// <summary>
    /// Converts an <see cref="UserModel"/> to an <see cref="UserResponse"/>.
    /// </summary>
    /// <param name="model">The model to convert.</param>
    /// <returns>A <see cref="UserResponse"/> based on the model.</returns>
    public static UserResponse ToUserResponse(this UserModel model)
    {
        return new UserResponse(model.Id, model.UserName, model.Email, model.CreateDate, model.ModifyDate);
    }

    /// <summary>
    /// Converts an <see cref="UserRequest"/> to an <see cref="UserEdit"/>.
    /// </summary>
    /// <param name="request">The request to convert.</param>
    /// <returns>A <see cref="UserEdit"/> based on the request.</returns>
    public static UserEdit ToUserEdit(this UserRequest request)
    {
        return new UserEdit(request.email, request.username, request.password);
    }
}
