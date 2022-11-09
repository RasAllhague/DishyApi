using DishyApi.Models.Dishes;
using DishyApi.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DishyApi.Controllers;

public readonly record struct DishRequest(string name, string? description, string? notes, int imageId);

[Route("api/[controller]")]
[ApiController]
public class DishesController : LoggingControllerBase
{
    private readonly ITokenService _tokenService;

    public DishesController(ILogger logger, ITokenService tokenService) : base(logger)
    {
        _tokenService = tokenService;
    }

    // GET: api/<DishesController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<DishModel>>> GetDishesAsync()
    {
        throw new NotImplementedException();
    }

    // GET api/<DishesController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<DishModel>> GetDishAsync(int id)
    {
        throw new NotImplementedException();
    }

    // POST api/<DishesController>
    [HttpPost]
    public async Task<ActionResult> CreateDishAsync([FromBody] DishRequest request)
    {
        throw new NotImplementedException();
    }

    // PUT api/<DishesController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateDishAsync(int id, [FromBody] DishRequest request)
    {
        throw new NotImplementedException();
    }

    // DELETE api/<DishesController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteDishAsync(int id)
    {
        throw new NotImplementedException();
    }
}
