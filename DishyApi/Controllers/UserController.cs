using DishyApi.Extensions;
using DishyApi.Models.User;
using DishyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishyApi.Controllers;

public readonly record struct UserResponse(int id, string username, string email, DateTime createDate, DateTime? modifyDate);
public readonly record struct UserRequest(string username, string email, string password, DateTime createDate, DateTime? modifyDate);

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UserController : LoggingControllerBase
{
    private readonly IUserService _userService;

    public UserController(ILogger<UserController> logger, IUserService userService) : base(logger)
    {
        _userService = userService;
    }

    // GET: api/<UserController>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsersAsync()
    {
        _logger.LogDebug("All user informations have been requested.");

        var users = await _userService.GetUsersAsync();

        return Ok(users.Select(x => x.ToUserResponse()));
    }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<UserResponse>> GetUserAsync(int id)
    {
        _logger.LogDebug("User information for user with id {0} has been requested.", id);

        var user = await _userService.GetUserAsync(id);

        if (user is null)
        {
            return NotFound();
        }

        return user.ToUserResponse();
    }

    // POST api/<UserController>
    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UserRequest request)
    {
        _logger.LogInformation("The creation of a new user has been requested.");

        if (await _userService.ExistsAsync(request.email))
        {
            return BadRequest();
        }

        var new_user = await _userService.CreateUserAsync(request.ToUserModel());

        _logger.LogInformation("A new user has been created with id {0}.", new_user.Id);

        return Ok();
    }

    // PUT api/<UserController>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponse>> Put(int id, [FromBody] UserRequest request)
    {
        _logger.LogDebug("User modification has been requested.");

        if (!await _userService.ExistsAsync(request.email))
        {
            return NotFound();
        }

        var updatedUser = await _userService.UpdateUserAsync(request.ToUserEdit());

        return Ok(updatedUser.ToUserResponse());
    }

    // DELETE api/<UserController>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        _logger.LogWarning("The deletion of the with {0} user has been requested", id);

        if (!await _userService.ExistsAsync(id))
        {
            return NotFound();
        }

        if (!await _userService.DeleteUserAsync(id))
        {
            return Unauthorized("You have insufficient rights for this action.");
        }

        _logger.LogWarning("A user has been deleted.", id);

        return Ok();
    }
}
