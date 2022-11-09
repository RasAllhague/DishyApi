using DishyApi.Models.Categories;
using DishyApi.Models.Ingredients;

namespace DishyApi.Services.Ingredients;

public interface IIngredientCategoryService
{
    Task<IEnumerable<CategoryModel>> GetAllIngredientCategoriesAsync(int id);
    Task<IEnumerable<CategoryModel>> GetCategoriesOfIngredientAsync(int userId, int id);
    Task<bool> AddIngredientToCategoryAsync(int userId, int ingredientId, int categoryId);
    Task<bool> RemoveIngredientFromCategoryAsync(int userId, int ingredientId, int categoryId);
}
