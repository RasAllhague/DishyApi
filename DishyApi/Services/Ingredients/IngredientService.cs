using Dapper;
using DishyApi.Models.Ingredients;
using Microsoft.AspNetCore.Diagnostics;
using MySqlConnector;

namespace DishyApi.Services.Ingredients;

public class IngredientService : IIngredientService
{
    private readonly IDbConnService _dbConnService;

    public IngredientService(IDbConnService dbConnService)
    {
        _dbConnService = dbConnService;
    }

    public async Task<int> CreateIngredientAsync(IngredientModel newIngredient)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();

        string existsSql =
            "SELECT * " +
            "FROM Ingredients " +
            "WHERE CreateUserId = @userId " +
            "AND Name = @name";

        if ((await conn.QueryAsync(existsSql, new { name = newIngredient.Name, userId = newIngredient.CreateUserId })).Any())
        {
            return 0;
        }

        string insertSql =
            "INSERT INTO Ingredients " +
            "(Name, Description, Notes, ImageId, CreateUserId, CreateDate) " +
            "VALUES " +
            "(@name, @description, @notes, @imageId, @createUserId, @createDate);";

        await conn.ExecuteAsync(insertSql, newIngredient);

        string selectSql =
            "SELECT Id " +
            "FROM Ingredients AS i " +
            "WHERE CreateUserId = @userId " +
            "ORDER BY CreateDate DESC " +
            "LIMIT 1;";

        int? id = await conn.QueryFirstOrDefaultAsync<int>(selectSql, new { @userId = newIngredient.CreateUserId });

        return id.HasValue ? id.Value : 0;
    }

    public async Task<bool> DeleteIngredientAsync(int userId, int id)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();
        string sql = "DELETE FROM Ingredients WHERE Id = @id AND CreateUserId = @userId";

        int affectedRows = await conn.ExecuteAsync(sql, new { id, userId });

        return affectedRows > 0;
    }

    public async Task<IngredientModel?> GetIngredientAsync(int userId, int id)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();
        string sql =
            "SELECT * " +
            "FROM Ingredients " +
            "WHERE CreateUserId = @userId " +
            "AND Id = @id";

        return await conn.QueryFirstOrDefaultAsync<IngredientModel>(sql, new { id, userId });
    }

    public async Task<IEnumerable<IngredientModel>> GetIngredientsAsync(int userId)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();
        string sql =
            "SELECT * " +
            "FROM Ingredients " +
            "WHERE CreateUserId = @id";

        return await conn.QueryAsync<IngredientModel>(sql, new { id = userId });
    }

    public async Task<IngredientModel?> UpdateIngredientAsync(int userId, int id, IngredientEdit editModel)
    {
        using MySqlConnection conn = _dbConnService.GetConnection();
        IngredientModel? oldIngredient = await GetIngredientAsync(userId, id);

        if (oldIngredient == null)
        {
            return null;
        }

        oldIngredient.Name = editModel.name;
        oldIngredient.Notes = editModel.notes;
        oldIngredient.Description = editModel.description;
        oldIngredient.ImageId = editModel.imageId;
        oldIngredient.ModifyUserId = userId;
        oldIngredient.ModifyDate = DateTime.Now;

        string sql =
            "UPDATE Ingredients " +
            "SET Name = @name, Notes = @notes, Description = @description, ImageId = @imageId, ModifyUserId = @modifyUserId, ModifyDate = @modifyDate " +
            "WHERE CreateUserId = @createUserId";

        await conn.ExecuteAsync(sql, oldIngredient);

        return await GetIngredientAsync(userId, id);
    }
}
