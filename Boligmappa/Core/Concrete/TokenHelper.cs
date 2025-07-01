using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Core.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Core.Concrete;

public class TokenHelper : ITokenHelper
{
    private readonly IConfiguration _configuration;

    public TokenHelper(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateToken(int userId, string email, string role)
    {
        var claims = new List<Claim>
        {
            new Claim("userId", userId.ToString()),          // Özel claim: userId
            new Claim("userRole", role),                      // Özel claim: userRole
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()), // Standart claim
            new Claim(JwtRegisteredClaimNames.Email, email),          // Standart claim
            new Claim(ClaimTypes.Role, role)                  // Standart claim
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}