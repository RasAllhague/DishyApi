using DishyApi.Models.Categories;
using DishyApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DishyApi.Controllers;

public readonly record struct NewCategoryRequest(string name, string? description);

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : LoggingControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ITokenService _tokenService;

    public CategoriesController(ILogger logger, ICategoryService categoryService, ITokenService tokenService) : base(logger)
    {
        _categoryService = categoryService;
        _tokenService = tokenService;
    }

    [HttpPost("Dishes")]
    public async Task<ActionResult<CategoryModel>> CreateCategoryForDishesAsync([FromBody] NewCategoryRequest request)
    {
        return await CreateCategoryForType(request, CategoryType.Dish);
    }

    [HttpPost("Ingredients")]
    public async Task<ActionResult<CategoryModel>> CreateCategoryForIngredientsAsync([FromBody] NewCategoryRequest request)
    {
        return await CreateCategoryForType(request, CategoryType.Ingredient);
    }

    [HttpPost("Foodplans")]
    public async Task<ActionResult<CategoryModel>> CreateCategoryForFoodplansAsync([FromBody] NewCategoryRequest request)
    {
        return await CreateCategoryForType(request, CategoryType.Foodplan);
    }

    private async Task<ActionResult<CategoryModel>> CreateCategoryForType(NewCategoryRequest request, CategoryType categoryType)
    {
        var authUser = await _tokenService.RetrievedUserFromTokenAsync(HttpContext);
        if (authUser is null)
        {
            return Unauthorized();
        }

        CategoryModel? newCategory = new CategoryModel()
        {
            Id = 0,
            CreateUserId = authUser.Id,
            CreateDate = DateTime.Now,
            CategoryTypeId = (int)categoryType,
            Description = request.description,
            Name = request.name,
        };

        newCategory = await _categoryService.CreateCategoryAsync(newCategory, categoryType);

        if (newCategory is null)
        {
            return BadRequest($"Category already exists in {categoryType}.");
        }

        return Ok(newCategory);
    }
}
