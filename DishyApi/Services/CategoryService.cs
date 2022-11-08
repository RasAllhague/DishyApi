using Dapper;
using DishyApi.Models.Categories;
using MySqlConnector;

namespace DishyApi.Services;

public class CategoryService : ICategoryService
{
    private readonly IDbConnService _dbConnService;

    public CategoryService(IDbConnService dbConnService)
    {
        _dbConnService = dbConnService;
    }

    public async Task<CategoryModel?> CreateCategoryAsync(CategoryModel newCategoryModel, CategoryType categoryType)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();
        CategoryModel? categoryModel = await GetCategoryByNameAsync(conn, newCategoryModel.CreateUserId, newCategoryModel.Name, categoryType);

        if (categoryModel != null)
        {
            return null;
        }

        string insertSql =
            "INSERT INTO Categories " +
            "(Name, Description, CategoryTypeId, CreateDate, CreateUserId) " +
            "VALUES " +
            "(@name, @description, @categoryTypeId, @createDate, @createUserId);";

        await conn.ExecuteAsync(insertSql, new
        {
            name = newCategoryModel.Name,
            description = newCategoryModel.Description,
            categoryTypeId = (int)categoryType,
            createDate = DateTime.Now,
            createUserId = newCategoryModel.CreateUserId
        });

        return await GetCategoryByNameAsync(conn, newCategoryModel.CreateUserId, newCategoryModel.Name, categoryType);
    }

    private async Task<CategoryModel?> GetCategoryByNameAsync(MySqlConnection conn, int userId, string name, CategoryType categoryType)
    {
        string selectSql =
                    "SELECT * " +
                    "FROM Categories " +
                    "WHERE Name = @name " +
                    "AND CreateUserId = @userId " +
                    "AND CategoryTypeId = @categoryTypeId";

        var categoryModel = await conn.QueryFirstOrDefaultAsync<CategoryModel>(selectSql, new { name, userId, categoryTypeId = (int)categoryType });

        return categoryModel;
    }

    public async Task<bool> DeleteCategoryAsync(int userId, int id)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();
        string sql =
            "DELETE FROM Categories " +
            "WHERE CreateUserId = @userId " +
            "AND Id = @id;";

        var affectedRows = await conn.ExecuteAsync(sql, new { userId, id });

        return affectedRows > 0;
    }
}