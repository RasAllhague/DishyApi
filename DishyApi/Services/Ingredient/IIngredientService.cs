using DishyApi.Models.Ingredients;

namespace DishyApi.Services.Ingredient;

public readonly record struct IngredientEdit(string name, string? description, string? notes, int imageId);

public interface IIngredientService
{
    Task<IEnumerable<IngredientModel>> GetIngredientsAsync(int userId);
    Task<IngredientModel?> GetIngredientAsync(int userId, int id);
    Task<int> CreateIngredientAsync(IngredientModel newIngredient);
    Task<IngredientModel?> UpdateIngredientAsync(int userId, int id, IngredientEdit editModel);
    Task<bool> DeleteIngredientAsync(int userId, int id);
}
