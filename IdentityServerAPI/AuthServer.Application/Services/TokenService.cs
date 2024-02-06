using AuthServer.Core.Entities;
using AuthServer.Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthServer.Application.Services;

public class TokenService : ITokenService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;
    public TokenService(UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    public async Task<string> GenerateToken(string username)
    {
        var user = await _userManager.FindByNameAsync(username) ?? throw new Exception("User not found"); 

        var claims = new List<Claim>
        {
            new Claim("Username", user.UserName),
            new Claim("Email", user.Email),
            new Claim("Id", user.Id)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSecret"]));

        var sigInCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken
                (
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: sigInCredentials,
                    issuer: _configuration["JWTIssuer"]
                );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}