using DishyApi.Extensions;
using DishyApi.Models.Categories;
using DishyApi.Models.Ingredients;
using DishyApi.Services;
using DishyApi.Services.Ingredient;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.IdentityModel.Tokens.Jwt;

namespace DishyApi.Controllers;

public readonly record struct NewIngredientRequest(string name, string? description, string? notes, int imageId);

/// <summary>
/// Api controller for ingredient operations.
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class IngredientsController : LoggingControllerBase
{
    private readonly IIngredientService _ingredientService;
    private readonly ITokenService _tokenService;
    private readonly IIngredientCategoryService _ingredientCategoryService;

    public IngredientsController(ILogger<IngredientsController> logger, IIngredientService ingredientService, ITokenService tokenService, IIngredientCategoryService ingredientCategoryService) : base(logger)
    {
        _ingredientService = ingredientService;
        _tokenService = tokenService;
        _ingredientCategoryService = ingredientCategoryService;
    }

    // GET: api/<IngredientsController>
    /// <summary>
    /// Gets all ingredients of a user identified by the jwt.
    /// </summary>
    /// <returns><see cref="IEnumerable{T}"/> which contains <see cref="IngredientModel"/> instances.</returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<IngredientModel>>> GetIngredientsAsync()
    {
        var authUser = await _tokenService.RetrievedUserFromTokenAsync(HttpContext);
        if (authUser is null)
        {
            return Unauthorized();
        }

        _logger.LogDebug("List of all ingredients for user '{0}' requested.", authUser.Id);

        var ingredients = await _ingredientService.GetIngredientsAsync(authUser.Id);

        return Ok(ingredients);
    }

    // GET api/<IngredientsController>/5
    /// <summary>
    /// Get a ingredient from a user identified by an id.
    /// </summary>
    /// <param name="id">The id of the ingredient.</param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    [HttpGet("{id}")]
    public async Task<ActionResult<IngredientModel?>> GetIngredientAsync(int id)
    {
        var authUser = await _tokenService.RetrievedUserFromTokenAsync(HttpContext);
        if (authUser is null)
        {
            return Unauthorized();
        }

        var ingredient = await _ingredientService.GetIngredientAsync(authUser.Id, id);

        if (ingredient is null)
        {
            return NotFound("Ingredient not found.");
        }

        return Ok(ingredient);
    }

    // POST api/<IngredientsController>
    [HttpPost]
    public async Task<ActionResult<int>> CreateIngredientAsync([FromBody] NewIngredientRequest request)
    {
        var authUser = await _tokenService.RetrievedUserFromTokenAsync(HttpContext);
        if (authUser is null)
        {
            return Unauthorized();
        }

        var ingredient = request.ToIngredientModel();
        ingredient.CreateDate = DateTime.Now;
        ingredient.CreateUserId = authUser.Id;

        int id = await _ingredientService.CreateIngredientAsync(ingredient);

        if (id == 0)
        {
            return BadRequest("Ingredient already exists.");
        }

        return Ok(id);
    }

    // PUT api/<IngredientsController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<IngredientModel?>> EditIngredientAsync(int id, [FromBody] IngredientEdit editModel)
    {
        var authUser = await _tokenService.RetrievedUserFromTokenAsync(HttpContext);
        if (authUser is null)
        {
            return Unauthorized();
        }

        IngredientModel? updatedIngredient = await _ingredientService.UpdateIngredientAsync(authUser.Id, id, editModel);

        if (updatedIngredient is null)
        {
            return NotFound("Ingredient not found.");
        }

        return Ok(updatedIngredient);
    }

    // DELETE api/<IngredientsController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteIngredientAsync(int id)
    {
        var authUser = await _tokenService.RetrievedUserFromTokenAsync(HttpContext);
        if (authUser is null)
        {
            return Unauthorized();
        }

        if (!await _ingredientService.DeleteIngredientAsync(authUser.Id, id))
        {
            return NotFound("The ingredient to delete was not found.");
        }

        return Ok();
    }

    // GET api/<IngredientsController>/5/Categories/
    [HttpGet("{id}/Categories")]
    public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoriesOfIngredientAsync(int id)
    {
        var authUser = await _tokenService.RetrievedUserFromTokenAsync(HttpContext);
        if (authUser is null)
        {
            return Unauthorized();
        }

        var categories = await _ingredientCategoryService.GetCategoriesOfIngredientAsync(authUser.Id, id);

        return Ok(categories);
    }

    // GET api/<IngredientsController>/Categories
    [HttpGet("Categories")]
    public async Task<ActionResult<IEnumerable<CategoryModel>>> GetIngredientCategoriesAsync()
    {
        var authUser = await _tokenService.RetrievedUserFromTokenAsync(HttpContext);
        if (authUser is null)
        {
            return Unauthorized();
        }

        var categories = await _ingredientCategoryService.GetAllIngredientCategoriesAsync(authUser.Id);

        return Ok(categories);
    }

    [HttpPut("{idIngredient}/Caregories/{idCategory}")]
    public async Task<ActionResult> AddIngredientToCategoryAsync(int idIngredient, int idCategory)
    {
        var authUser = await _tokenService.RetrievedUserFromTokenAsync(HttpContext);
        if (authUser is null)
        {
            return Unauthorized();
        }

        if (!await _ingredientCategoryService.AddIngredientToCategoryAsync(authUser.Id, idIngredient, idCategory))
        {
            return BadRequest("Ingredient category with this combination already exists.");
        }

        return Ok();
    }
}
