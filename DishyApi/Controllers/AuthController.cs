using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DishyApi.Controllers;

public readonly record struct LoginRequest(string email, string password);

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    [HttpGet("/Logout")]
    public async Task<ActionResult> Get()
    {
        return BadRequest();
    }

    // POST api/<AuthController>
    [HttpPost("/Login")]
    public async Task<ActionResult> Post([FromBody] LoginRequest request)
    {
        return BadRequest();
    }
}
