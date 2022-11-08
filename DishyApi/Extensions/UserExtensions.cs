using DishyApi.Controllers;
using DishyApi.Models.User;
using DishyApi.Services;
using System.Runtime.CompilerServices;

namespace DishyApi.Extensions;

public static class UserExtensions
{
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

    public static UserResponse ToUserResponse(this UserModel model)
    {
        return new UserResponse(model.Id, model.UserName, model.Email, model.CreateDate, model.ModifyDate);
    }

    public static UserEdit ToUserEdit(this UserRequest request)
    {
        return new UserEdit(request.email, request.username, request.password);
    }
}
