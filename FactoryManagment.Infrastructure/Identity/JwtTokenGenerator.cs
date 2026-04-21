using FactoryManagment.Application.Abstractions.Identity;
using FactoryManagment.Domain.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace FactoryManagment.Infrastructure.Identity;

public class JwtTokenGenerator(IOptions<JwtSettings> jwtOptions) : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings = jwtOptions.Value;

    public string GenerateToken(string userId, string email, string firstName, string lastName, IEnumerable<string> roles, out int expiresIn)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var SecretKey = Encoding.UTF8.GetBytes(_jwtSettings.SecretKey);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.Email, email),
            new("FirstName", firstName),
            new("LastName", lastName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.Add(new Claim("Roles", JsonSerializer.Serialize(roles), JsonClaimValueTypes.JsonArray));

        expiresIn = _jwtSettings.ExpiryMinutes * 60;

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddSeconds(expiresIn),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(SecretKey), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

    public RefreshToken GenerateRefreshToken()
    {
        var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
        return new RefreshToken(token, DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpiryDays), DateTime.UtcNow);
    }
}
