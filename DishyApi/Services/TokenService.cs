using DishyApi.Configuration;
using DishyApi.Models.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
namespace DishyApi.Services;

public class TokenService : ITokenService
{
    private readonly JwtSettings _jwtSettings;
    private readonly IUserService _userService;
    
    public TokenService(IOptions<JwtSettings> jwtSettings, IUserService userService)
    {
        _jwtSettings = jwtSettings.Value;
        _userService = userService;
    }

    public string CreateToken(UserModel userModel)
    {
        byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.Key);

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new System.Security.Claims.ClaimsIdentity(new[]
            {
                new Claim("Id", Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, userModel.UserName),
                new Claim(JwtRegisteredClaimNames.Email, userModel.Email),
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
        return tokenHandler.WriteToken(token);
    }

    public async Task<UserModel?> RetrievedUserFromTokenAsync(HttpContext context)
    {
        var stringToken = await context.GetTokenAsync("access_token");

        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(stringToken);

        var claim = jwtToken.Claims.FirstOrDefault(x => x.Type == "email");

        if (claim is null) 
        {
            return null;
        }

        return await _userService.GetUserAsync(claim.Value);
    }
}
