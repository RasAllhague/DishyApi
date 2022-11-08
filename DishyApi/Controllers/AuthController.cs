using DishyApi.Configuration;
using DishyApi.Models.User;
using DishyApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DishyApi.Controllers;

/// <summary>
/// Model record for login request data.
/// </summary>
/// <param name="email">The email used in the loging request.</param>
/// <param name="password">The password used in the login request.</param>
public readonly record struct LoginRequest(string email, string password);

/// <summary>
/// Controller for authentication actions. Generates a jwt token and logs the user out.
/// </summary>
[Route("api/[controller]")]
[Authorize]
[ApiController]
public class AuthController : LoggingControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtSettings _jwtSettings;

    /// <summary>
    /// Creates a new <see cref="AuthController"/> instance and injects all nessecary services.
    /// </summary>
    /// <param name="logger">The service needed for logging.</param>
    /// <param name="userService">The service needed for user auth.</param>
    /// <param name="jwtSettings">The options needed for jwt generation settings.</param>
    public AuthController(ILogger<AuthController> logger, IUserService userService, IOptions<JwtSettings> jwtSettings) : base(logger)
    {
        _userService = userService;
        _jwtSettings = jwtSettings.Value;
    }

    // GET api/<AuthController>/Logout
    /// <summary>
    /// Logs the user out.
    /// </summary>
    /// <returns><see cref="BadRequestResult"/></returns>
    [HttpGet("Logout")]
    public async Task<ActionResult> Get()
    {
        return BadRequest();
    }


    // POST api/<AuthController>/Login
    /// <summary>
    /// Logs the user in and creates a jwt token for further authorization.
    /// </summary>
    /// <param name="request">The login request made to the api.</param>
    /// <returns>An <see cref="UnauthorizedResult"/> if verfication failed or an <see cref="OkResult"/> with a encrypted jwt token string.</returns>
    [AllowAnonymous]
    [HttpPost("Login")]
    public async Task<ActionResult> Post([FromBody] LoginRequest request)
    {
        _logger.LogInformation("User '{0}' has requested a login token.", request.email);

        if (!await _userService.VerifyLoginAsync(request.email, request.password))
        {
            _logger.LogWarning("Login for user {0} has failed because verification failure.", request.email);
            return Unauthorized();
        }

        UserModel? user = await _userService.GetUserAsync(request.email);

        if (user is null)
        {
            _logger.LogWarning("Login for user {0} has failed because user was not found.", request.email);
            return Unauthorized();
        }

        byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),
            Expires = DateTime.UtcNow.AddMinutes(30),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);
        var stringToken = tokenHandler.WriteToken(token);

        return Ok(stringToken);
    }
}
