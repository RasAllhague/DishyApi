using DishyApi.Models.Foodplans;
using DishyApi.Services;
using DishyApi.Services.Foodplan;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Eventing.Reader;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DishyApi.Controllers;

public readonly record struct NewFoodplanRequest(string name, string description, int imageId, bool notifyUsers, bool deactivated);
public readonly record struct EditFoodplanRequest(string name, string description, int imageId, bool notifyUsers, bool deactivated, int owningUserId);

[Route("api/[controller]")]
[ApiController]
public class FoodplansController : LoggingControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IFoodplanService _foodplanService;

    public FoodplansController(ILogger<FoodplansController> logger, ITokenService tokenService, IFoodplanService foodplanService) : base(logger)
    {
        _tokenService = tokenService;
        _foodplanService = foodplanService;
    }

    // GET: api/<FoodplansController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FoodplanModel>>> GetFoodplansAsync()
    {
        throw new NotImplementedException();
    }

    // GET api/<FoodplansController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FoodplanModel>> GetFoodplanAsync(int id)
    {
        throw new NotImplementedException();
    }

    // POST api/<FoodplansController>
    [HttpPost]
    public async Task<ActionResult> CreateFoodplanAsync([FromBody] NewFoodplanRequest request)
    {
        throw new NotImplementedException();
    }

    // PUT api/<FoodplansController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateFoodplanAsync(int id, [FromBody] EditFoodplanRequest request)
    {
        throw new NotImplementedException();
    }

    // DELETE api/<FoodplansController>/5
    [HttpDelete("{id}")]
    public Task<ActionResult> DeleteFoodplanAsync(int id)
    {
        throw new NotImplementedException();
    }
}
