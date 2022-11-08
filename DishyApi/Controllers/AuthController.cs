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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DishyApi.Controllers;

public readonly record struct LoginRequest(string email, string password);

[Route("api/[controller]")]
[ApiController]
public class AuthController : LoggingControllerBase
{
    private readonly IUserService _userService;
    private readonly JwtSettings _jwtSettings;

    public AuthController(ILogger<AuthController> logger, IUserService userService, IOptions<JwtSettings> jwtSettings) : base(logger)
    {
        _userService = userService;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpGet("Logout")]
    public async Task<ActionResult> Get()
    {
        return BadRequest();
    }

    // POST api/<AuthController>
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
