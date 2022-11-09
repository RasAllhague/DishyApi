using DishyApi.Models.Categories;
using DishyApi.Models.User;
using System.Xml.Linq;

namespace DishyApi.Services;

public interface ICategoryService
{
    Task<CategoryModel?> CreateCategoryAsync(CategoryModel newCategoryModel, CategoryType categoryType);
    Task<bool> DeleteCategoryAsync(int userId, int id);
}