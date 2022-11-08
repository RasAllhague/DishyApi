using DishyApi.Extensions;
using DishyApi.Models.User;
using DishyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DishyApi.Controllers;

/// <summary>
/// A response message with user data.
/// </summary>
/// <param name="id">Id of the user</param>
/// <param name="username">Name of the user.</param>
/// <param name="email">Email of the user.</param>
/// <param name="createDate">Creation date of the user.</param>
/// <param name="modifyDate">Last modification date of the user.</param>
public readonly record struct UserResponse(int id, string username, string email, DateTime createDate, DateTime? modifyDate);
/// <summary>
/// A request message with the user data.
/// </summary>
/// <param name="username">Name of the user.</param>
/// <param name="email">Email of the user.</param>
/// <param name="password">Password of the user.</param>
/// <param name="createDate">Creation date of the user.</param>
/// <param name="modifyDate">Last modification date of the user.</param>
public readonly record struct UserRequest(string username, string email, string password, DateTime createDate, DateTime? modifyDate);

/// <summary>
/// Api controller for user operations.
/// </summary>
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class UsersController : LoggingControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    /// Creates a new instance of <see cref="UsersController"/> and injects an <see cref="IUserService"/> instance.
    /// </summary>
    /// <param name="logger">The logger needed for logging.</param>
    /// <param name="userService">The user service needed to perform user operations.</param>
    public UsersController(ILogger<UsersController> logger, IUserService userService) : base(logger)
    {
        _userService = userService;
    }

    // GET: api/<UsersController>
    /// <summary>
    /// Gets all users which exist in the database.
    /// </summary>
    /// <returns>An <see cref="OkResult"/> which contains an <see cref="IEnumerable{T}"/> of type <see cref="UserResponse"/>.</returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> GetUsersAsync()
    {
        _logger.LogDebug("All user informations have been requested.");

        var users = await _userService.GetUsersAsync();

        return Ok(users.Select(x => x.ToUserResponse()));
    }

    // GET api/<UsersController>/5
    /// <summary>
    /// Gets a single user by an specific id.
    /// </summary>
    /// <param name="id">The id of the user which should be retrieved.</param>
    /// <returns>An <see cref="OkResult"/> containing a <see cref="UserResponse"/> if found or else a <see cref="NotFoundResult"/>.</returns>
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

    // POST api/<UsersController>
    /// <summary>
    /// Creates a new user.
    /// </summary>
    /// <param name="request">The userinformation for the creation.</param>
    /// <returns><see cref="OkResult"/> if successfull or a <see cref="BadRequestResult"/> if already exists.</returns>
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

    // PUT api/<UsersController>/5
    /// <summary>
    /// Updates the information of a user with given id.
    /// </summary>
    /// <param name="id">The id of the user which should be updated.</param>
    /// <param name="request">The data with which the user should be updated.</param>
    /// <returns><see cref="OkResult"/> containing a <see cref="UserResponse"/> or a <see cref="NotFoundResult"/> if the user was not found.</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<UserResponse>> Put(int id, [FromBody] UserRequest request)
    {
        _logger.LogDebug("User modification has been requested.");

        if (!await _userService.ExistsAsync(request.email))
        {
            return NotFound();
        }

        var updatedUser = await _userService.UpdateUserAsync(request.ToUserEdit());

        if (updatedUser is null)
        {
            return NotFound();
        }

        return Ok(updatedUser.ToUserResponse());
    }

    // DELETE api/<UsersController>/5
    /// <summary>
    /// Deletes a user with a given id from the database.
    /// </summary>
    /// <param name="id">The id of the user to delete.</param>
    /// <returns><see cref="OkResult"/> if successfull, <see cref="NotFoundResult"/> if not found or <see cref="UnauthorizedResult"/> if the rigths are not given for the current user.</returns>
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
