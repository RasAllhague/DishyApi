using DishyApi.Controllers;
using DishyApi.Models.Ingredients;
using DishyApi.Services;
using System.Runtime.CompilerServices;

namespace DishyApi.Extensions;

public static class IngredientExtensions
{
    public static IngredientModel ToIngredientModel(this NewIngredientRequest request)
    {
        return new IngredientModel()
        {
            Name = request.name,
            Description = request.description,
            Notes = request.notes,
            ImageId = request.imageId == 0 ? null : request.imageId,
        };
    }
}
