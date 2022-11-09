using Dapper;
using DishyApi.Models.Categories;
using MySqlConnector;

namespace DishyApi.Services.Ingredient;

public class IngredientCategoryService : IIngredientCategoryService
{
    private readonly IDbConnService _dbConnService;

    public IngredientCategoryService(IDbConnService dbConnService)
    {
        _dbConnService = dbConnService;
    }

    public async Task<IEnumerable<CategoryModel>> GetAllIngredientCategoriesAsync(int id)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        string sql =
            "SELECT * " +
            "FROM Categories " +          
            "WHERE CreateUserId = @userId " +
            "AND CategoryTypeId = 2;";

        return await conn.QueryAsync<CategoryModel>(sql, new { id });
    }

    public async Task<IEnumerable<CategoryModel>> GetCategoriesOfIngredientAsync(int userId, int id)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        string sql =
            "SELECT c.Id, c.Name, c.Description, c.CategoryTypeId, c.CreateDate, c.CreateUserId, c.ModifyDate, c.ModifyUserId " +
            "FROM Categories AS c " +
            "INNER JOIN IngredientCategories AS ic " +
            "ON c.Id = ic.CategoryId " +
            "WHERE ic.IngredientId = @id " +
            "AND ic.CreateUserId = @userId " +
            "AND c.CreateUserId = @userId " +
            "AND c.CategoryTypeId = 2;";

        return await conn.QueryAsync<CategoryModel>(sql, new { userId, id });
    }

    public async Task<bool> AddIngredientToCategoryAsync(int userId, int ingredientId, int categoryId)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        if (await IngredientCategoryExists(conn, userId, ingredientId, categoryId))
        {
            return false;
        }

        string sql =
            "INSERT INTO IngredientCategories " +
            "(IngredientId, CategoryId, CreateUserId, CreateDate) " +
            "VALUES " +
            "(@ingredientId, @categoryId, @createUserId, @createDate);";

        await conn.ExecuteAsync(sql, new { userId, ingredientId, categoryId, createDate = DateTime.Now});

        return true;
    }

    public async Task<bool> RemoveIngredientFromCategoryAsync(int userId, int ingredientId, int categoryId)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        if (!await IngredientCategoryExists(conn, userId, ingredientId, categoryId))
        {
            return false;
        }

        string sql =
            "DELETE FROM IngredientCategories " +
            "WHERE CreateUserId = @userId " +
            "AND IngredientId = @ingredientId " +
            "AND CategoryId = @categoryId;";

        await conn.ExecuteAsync(sql, new { userId, ingredientId, categoryId });

        return true;
    }

    private async Task<bool> IngredientCategoryExists(MySqlConnection conn, int userId, int ingredientId, int categoryId)
    {
        string sql = 
            "SELECT * " +
            "FROM IngredientCategories " +
            "WHERE CreateUserId = @userId " +
            "AND IngredientId = @ingredientId " +
            "AND CategoryId = @categoryId;";

        CategoryModel model = await conn.QueryFirstOrDefaultAsync<CategoryModel>(sql, new { userId, ingredientId, categoryId });
        
        return model != null;
    }
}
